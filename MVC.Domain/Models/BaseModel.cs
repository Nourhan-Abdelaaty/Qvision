using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Domain.Models;
public class BaseModel
{
    public BaseModel()
    {
        IsActive = true;
        CreatedDate = DateTime.Now;
    }
    public int Id { get; set; }
    public Guid? Guid { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public string? NameEn { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedById { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public int ModifyById { get; set; }
    public int ModifyCount { get; set; }
    public string? ModifyByName { get; set; }
    public string? Notes { get; set; }
}
