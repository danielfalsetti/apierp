using Dapper.Contrib.Extensions;
using System;

[Table("tb_alcada")]
public class Alcada
{
    public long id { get; set; }
    public String s_descricao { get; set; }
}