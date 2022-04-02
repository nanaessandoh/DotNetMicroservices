using System.ComponentModel.DataAnnotations;

namespace CommandService.Data.Models;

public abstract class BaseModel<TEntity> where TEntity : BaseModel<TEntity>
{
    public virtual bool HasRequiredFields()
    {
        return !this.GetType().GetProperties()
            .Where(_ => Attribute.IsDefined(_, typeof(RequiredAttribute)))
            .Where(_ => _.PropertyType == typeof(string))
            .Select(_ => _.GetValue(this)?.ToString())
            .Any(_ => string.IsNullOrWhiteSpace(_));
    }
}
