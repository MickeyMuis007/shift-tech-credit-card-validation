using System;

namespace SharedKernel.Models {
  public abstract class ViewModel<TId> : IEquatable<ViewModel<TId>> {
    public TId Id { get; protected set; }

    public override bool Equals(object obj) {
      var entity = obj as ViewModel<TId>;
      if (entity != null) {
        return Equals(entity);
      }
      return base.Equals(obj);
    }

    public bool Equals(ViewModel<TId> other) {
      if (other == null) {
        return false;
      }
      return Id.Equals(other.Id);
    }

    public override int GetHashCode(){ 
      return Id.GetHashCode();
    }
  }
}