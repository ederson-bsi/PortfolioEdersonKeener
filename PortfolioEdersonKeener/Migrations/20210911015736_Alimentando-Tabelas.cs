using Microsoft.EntityFrameworkCore.Migrations;

namespace PortfolioEdersonKeener.Migrations
{
    public partial class AlimentandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var function = @"INSERT INTO Categoria(Nome) VALUES('Mercearia'), ('Limpeza'), ('Higiene Pessoal'), ('Perecíveis')";
            migrationBuilder.Sql(function);

            var function2 = @"INSERT INTO Marca(Nome) VALUES('OMO'), ('LUX'), ('CLOSEUP'), ('NESTLE'), ('COCA-COLA'), ('PERDIGÃO'), ('SADIA'), ('QUALY'), ('CRISTAL'), ('COCAL-MIRIM')";
            migrationBuilder.Sql(function2);

            var function3 = @"INSERT INTO Unidade_Medida(Descricao) VALUES('Kg'), ('ML'), ('Unidade'), ('Caixa'), ('Litro'), ('Gramas')";
            migrationBuilder.Sql(function3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
