namespace Audicon.Infra.RelationalData.Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } 
        public Guid Key { get; set; } = Guid.NewGuid(); 
        public DateTime DataCriacao { get; set; } 
        public DateTime? DataAlteracao { get; set; } 
        public Guid UsuarioCriacao { get; set; } 
        public Guid? UsuarioAlteracao { get; set; } 
    }
}