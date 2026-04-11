namespace StudyBuddy.Domain.Entities
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; protected set; } = default!;
        public DateTime CreateDate { get; protected set; } = DateTime.Now;
        public DateTime? ModifyDate { get; protected set; } 
        public bool IsDeleted { get; protected set; }
        public DateTime? DeletedDate { get; protected set; }


        public virtual void Delete()
        {
            if (IsDeleted)
                return;
            IsDeleted = true;
            DeletedDate = DateTime.Now;
        }

        public virtual void Restore()
        {
            if (!IsDeleted) 
                return;
            IsDeleted = false;
            DeletedDate = null;
        }
        
    }
}
