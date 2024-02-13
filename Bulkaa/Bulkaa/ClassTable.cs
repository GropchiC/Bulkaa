namespace Bulkaa;

public class Продукция
{
    public int ID { get; set; }
    public string Название { get; set; }
    public int Цена { get; set; }
    public string Название_категории { get; set; }
}

public class Категория
{
    public int ID { get; set; }
    public string Название_категории { get; set; }
}