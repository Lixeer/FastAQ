namespace FastAQ.Models.Config.ElasticSearchConfig;

public class ElasticSearchConfig
{
    public required string ConnectionUrl { get; set; }
    public required string enable { get; set; }
    public required HttpAuth httpAuth { get; set; }
    public required int TimeoutSeconds { get; set; }

    
}

public class HttpAuth
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    
}