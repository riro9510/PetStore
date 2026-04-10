using PetStore.Data;
using PetStore.Models;
using Microsoft.EntityFrameworkCore;

// Interfaces/IDonationService.cs
public interface IDonationService
{
    Task<IEnumerable<Donation>> GetAllDonationsAsync();
    Task<Donation> CreateDonationAsync(Donation donation);
    Task UpdateDonationAsync(Donation donation);
    Task<IEnumerable<Donation>> GetRecentDonationsAsync();
}

// Services/DonationService.cs

public class DonationService : IDonationService
{
    private readonly PetStoreContext _context;

    public DonationService(PetStoreContext context)
    {
        _context = context;
    }

    public async Task<Donation> CreateDonationAsync(Donation donation)
    {
        donation.Id = Guid.NewGuid();
        donation.SubmittedAt = DateTime.UtcNow;
        donation.Status = DonationStatus.Pending;

        _context.Donations.Add(donation);
        await _context.SaveChangesAsync();
        return donation;
    }

    public async Task<IEnumerable<Donation>> GetAllDonationsAsync()
    {
        return await _context.Donations.ToListAsync();
    }

    public async Task UpdateDonationAsync(Donation donation)
    {
        if (donation == null) return;
        _context.Donations.Update(donation);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Donation>> GetRecentDonationsAsync()
    {
        return await _context.Donations
            .Where(d => d.Status == DonationStatus.Validated)
            .OrderByDescending(d => d.SubmittedAt)
            .Take(10)
            .ToListAsync();
    }
}