namespace Lager.Shared;

public sealed record TransactionDTO(TransactionType Type, int Count, string ArticleId, string Note);

public enum TransactionType
{
    Addition,
    Removal,
    Buying
}
