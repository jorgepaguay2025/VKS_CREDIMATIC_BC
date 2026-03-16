using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities
{
    [Table("MD_SYS_EMPRESAS")]
    public class MdSysEmpresa
    {
        [Column("Empresa")]
        [Required(ErrorMessage = "El código de empresa es obligatorio")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "La empresa debe tener entre 1 y 6 caracteres")]
        public string Empresa { get; set; }

        [Column("Ruc")]
        [StringLength(15, ErrorMessage = "El RUC no puede exceder los 15 caracteres")]
        public string? Ruc { get; set; }

        [Column("Razon_Social")]
        [StringLength(30, ErrorMessage = "La razón social no puede exceder los 30 caracteres")]
        public string? RazonSocial { get; set; }

        [Column("Razon_Comercial")]
        [StringLength(30, ErrorMessage = "La razón comercial no puede exceder los 30 caracteres")]
        public string? RazonComercial { get; set; }

        [Column("Ciudad")]
        [StringLength(30, ErrorMessage = "La ciudad no puede exceder los 30 caracteres")]
        public string? Ciudad { get; set; }

        [Column("Pais")]
        [StringLength(30, ErrorMessage = "El país no puede exceder los 30 caracteres")]
        public string? Pais { get; set; }

        [Column("Direccion")]
        [StringLength(50, ErrorMessage = "La dirección no puede exceder los 50 caracteres")]
        public string? Direccion { get; set; }

        [Column("Telefono_1")]
        [StringLength(15, ErrorMessage = "El teléfono 1 no puede exceder los 15 caracteres")]
        public string? Telefono1 { get; set; }

        [Column("Telefono_2")]
        [StringLength(15, ErrorMessage = "El teléfono 2 no puede exceder los 15 caracteres")]
        public string? Telefono2 { get; set; }

        [Column("Estado")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "El estado debe ser un solo carácter")]
        public string? Estado { get; set; }  // char(1) en SQL, lo representamos como string de longitud 1
    }
}
