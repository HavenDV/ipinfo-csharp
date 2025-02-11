﻿namespace IpInfo;

public partial class IpInfoApi
{
    /// <param name="ips"></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <summary>Returns information about the selected values.</summary>
    /// <returns>Batch response object.</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public async Task<IDictionary<string, FullResponse>> GetInformationByIpsAsync(
        IEnumerable<string> ips,
        CancellationToken cancellationToken)
    {
        var dictionary = await BatchAsync(ips, cancellationToken).ConfigureAwait(false);

        return dictionary.ToDictionary(
            pair => pair.Key,
            pair =>
                JsonSerializer.Deserialize<FullResponse>(pair.Value.ToString() ?? string.Empty) ??
                throw new InvalidOperationException($"FullResponse is null for {pair.Key}."));
    }
}
