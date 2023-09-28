namespace geradorDeEntidades.Generates;
public class GenerateInitalFolders
{
    public string SourcePath { get; private set; } = @"C:\dev\";
    public string EntityName { get; private set; }
    public DirectoryInfo EntityPath { get; private set; }
    public DirectoryInfo DomainPath { get; private set; }
    public DirectoryInfo ProviderPath { get; private set; }
    public DirectoryInfo UseCasePath { get; private set; }
    public DirectoryInfo TypeormPath { get; private set; }
    public DirectoryInfo TypeormSchemaPath { get; private set; }
    public DirectoryInfo DomainEntityPath { get; private set; }
    public DirectoryInfo DomainInputPath { get; private set; }
    public DirectoryInfo DomainRepositoryPath { get; private set; }
    public DirectoryInfo DomainValidatorPath { get; private set; }

    public GenerateInitalFolders(string entityName)
    {
        SourcePath += entityName;
        EntityName = entityName;
        if (Directory.Exists(SourcePath))
        {
            Console.WriteLine("Esse diretorio já existe");
            throw new Exception("Esse diretorio já existe");
        }

        CreateInitialFolders();
    }

    private void CreateInitialFolders()
    {
        //CRIA A PASTA DA ENTIDADE
        EntityPath = Directory.CreateDirectory(SourcePath);

        //CRIA AS PASTA DOMAIN E SUAS SUBPASTAS
        DomainPath = EntityPath.CreateSubdirectory("domain").CreateSubdirectory(EntityName);

        DomainEntityPath = DomainPath.CreateSubdirectory("entities");
        DomainInputPath = DomainPath.CreateSubdirectory("input");
        DomainRepositoryPath = DomainPath.CreateSubdirectory("repository");
        DomainValidatorPath = DomainPath.CreateSubdirectory("validator");

        //CRIA AS PASTA USECASE E SUAS SUBPASTAS
        UseCasePath = EntityPath.CreateSubdirectory("useCases").CreateSubdirectory(EntityName);

        //CRIA AS PASTA TYPEORM E SUAS SUBPASTAS
        TypeormPath = EntityPath.CreateSubdirectory("typeorm").CreateSubdirectory(EntityName);
        TypeormSchemaPath = TypeormPath.CreateSubdirectory("schemas");

        //CRIA AS PASTA PROVIDER E SUAS SUBPASTAS
        ProviderPath = EntityPath.CreateSubdirectory("providers");
    }
}
