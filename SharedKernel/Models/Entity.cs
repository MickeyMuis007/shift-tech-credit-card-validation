using System;

namespace SharedKernel.Models {
  public abstract class Entity<TId> : Entity, IEquatable<Entity<TId>> {
    public TId Id { get; protected set; }

    protected Entity(TId id) {
      if (object.Equals(id, default(TId))) {
        throw new ArgumentException("The Id cannot be the type's default value.", "id");
      }
      Id = id;
    }

    protected Entity() { }

    public override bool Equals(object obj) {
      var entity = obj as Entity<TId>;
      if (entity != null) {
        return Equals(entity);
      }
      return base.Equals(obj);
    }

    public bool Equals(Entity<TId> entity) {
      if (entity == null) {
        return false;
      }
      return true;
    }

    public override int GetHashCode() {
      return Id.GetHashCode();
    }
  }

  public abstract class Entity {
    public bool Active { get; protected set; }
    public string UserCreated { get; protected set; }
    public string LastUserUpdated { get; protected set; }
    public DateTime DateCreated { get; protected set; }
    public DateTime LastDateUpdated { get; protected set; }
  }
}