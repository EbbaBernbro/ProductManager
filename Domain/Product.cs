using System.ComponentModel.DataAnnotations;

namespace ProductManager.Domain;

class Product
{
  //Entiteten product behöver en primärnyckel
  public int Id { get; set; }

  [MaxLength(50)]
  public required string ProductName { get; set;}

  [MaxLength(50)]
  public required string SKU { get; set;}

  [MaxLength(100)]
  public required string Description { get; set;}

  [MaxLength(100)]
  public required string Image { get; set;}

  [MaxLength(10)]
  public required string Price { get; set;}

}