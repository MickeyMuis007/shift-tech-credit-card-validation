using System;

namespace SharedKernel.Models {
  public abstract class EntityBuilder<TId> {
    public TId Id { get; protected set; }

    public bool Active { get; protected set; }
    public string UserCreated { get; protected set; }
    public string LastUserUpdated { get; protected set; }
    public DateTime DateCreated { get; protected set; }
    public DateTime LastDateUpdated { get; protected set; }
  }
}