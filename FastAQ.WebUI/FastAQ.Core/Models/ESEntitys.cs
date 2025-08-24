
using System.Text.Json.Serialization;

namespace FastAQ.Models.ESEntitys;

public class ESSerchResponse<T> where T : class
{
    [JsonPropertyName("took")]
    public int Took { get; set; }

    [JsonPropertyName("timed_out")]
    public bool TimedOut { get; set; }

    [JsonPropertyName("_shards")]
    public Shards Shards { get; set; } = new();

    [JsonPropertyName("hits")]
    public Hits<T> Hits { get; set; } = new();
}

public class Shards
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("successful")]
    public int Successful { get; set; }

    [JsonPropertyName("skipped")]
    public int Skipped { get; set; }

    [JsonPropertyName("failed")]
    public int Failed { get; set; }
}

public class Hits<T> where T : class
{
    [JsonPropertyName("total")]
    public Total Total { get; set; } = new();

    [JsonPropertyName("max_score")]
    public double MaxScore { get; set; }

    [JsonPropertyName("hits")]
    public List<Hit<T>> HitList { get; set; } = new();
}

public class Total
{
    [JsonPropertyName("value")]
    public int Value { get; set; }

    [JsonPropertyName("relation")]
    public string Relation { get; set; } = string.Empty;
}

public class Hit<T> where T : class
{
    [JsonPropertyName("_index")]
    public string Index { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("_score")]
    public double Score { get; set; }

    [JsonPropertyName("_source")]
    public T Source { get; set; } = null!;
}

// 你的具体数据模型
public class AQDocument
{
    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("answer")]
    public string Answer { get; set; } = string.Empty;
}

public class ESInsertRespone
{
    [JsonPropertyName("_index")]
    public string Index { get; set; } = string.Empty;

    [JsonPropertyName("_id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("_version")]
    public int Version { get; set; }

    [JsonPropertyName("result")]
    public string Result { get; set; } = string.Empty;

    [JsonPropertyName("_shards")]
    public WriteShards Shards { get; set; } = new();

    [JsonPropertyName("_seq_no")]
    public int SequenceNumber { get; set; }

    [JsonPropertyName("_primary_term")]
    public int PrimaryTerm { get; set; }
}

public class WriteShards
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("successful")]
    public int Successful { get; set; }

    [JsonPropertyName("failed")]
    public int Failed { get; set; }
}

public class ESDeleteDocRespone
{
    [JsonPropertyName("took")]
    public long Took { get; set; }

    [JsonPropertyName("timed_out")]
    public bool TimedOut { get; set; }

    [JsonPropertyName("total")]
    public long Total { get; set; }

    [JsonPropertyName("deleted")]
    public long Deleted { get; set; }

    [JsonPropertyName("batches")]
    public long Batches { get; set; }

    [JsonPropertyName("version_conflicts")]
    public long VersionConflicts { get; set; }

    [JsonPropertyName("noops")]
    public long Noops { get; set; }

    [JsonPropertyName("retries")]
    public Retries Retries { get; set; } = new();

    [JsonPropertyName("throttled_millis")]
    public long ThrottledMillis { get; set; }

    [JsonPropertyName("requests_per_second")]
    public double RequestsPerSecond { get; set; }

    [JsonPropertyName("throttled_until_millis")]
    public long ThrottledUntilMillis { get; set; }

    [JsonPropertyName("failures")]
    public List<object> Failures { get; set; } = new();
}

public class Retries
{
    [JsonPropertyName("bulk")]
    public long Bulk { get; set; }

    [JsonPropertyName("search")]
    public long Search { get; set; }
}