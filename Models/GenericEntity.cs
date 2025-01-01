using SmartInventoryBE.ProjectAggregate.ViewModel;

namespace SmartInventoryBE.Models
{
    public class GenericBaseEntity<TModel, TEntity> : BaseEntity
        where TModel : BaseModel
        where TEntity : GenericBaseEntity<TModel, TEntity>
    {
        public virtual TModel ToModel(TModel model)
        {
            model.Id = Id;
            model.CreatedAt = CreatedAt;
            model.UpdatedAt = UpdatedAt;
            model.IsDeleted = IsDeleted;
            return model;
        }

        public virtual TEntity FromModel(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            Id = model.Id;
            CreatedAt = model.CreatedAt;
            UpdatedAt = model.UpdatedAt;
            IsDeleted = model.IsDeleted;

            return (TEntity)this;
        }

        public virtual void Patch(TEntity target)
        {
            target.CreatedAt = CreatedAt;
            target.UpdatedAt = UpdatedAt;
            target.IsDeleted = IsDeleted;
        }
    }
}
