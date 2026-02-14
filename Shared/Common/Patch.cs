namespace Shared.Common
{
    public record Patch<TKey, TDto>(TKey Id, TDto Data);

}
