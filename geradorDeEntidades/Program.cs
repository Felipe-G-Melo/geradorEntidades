using geradorDeEntidades.Generates;

Console.Write("DIGITE O NOME DA ENTIDADE: ");
string nomeDaEntidade = Console.ReadLine();

try
{
    //Generate Domain
    var folders = new GenerateInitalFolders(nomeDaEntidade);
    var generateFile = new GenerateDomainFile(
        nomeDaEntidade,
        folders.DomainEntityPath,
        folders.DomainInputPath,
        folders.DomainRepositoryPath,
        folders.DomainValidatorPath
        );
    generateFile.SetProperties();
    generateFile.SetInputProperts();
    generateFile.CreateEntityFile();
    generateFile.CreateInputFile();
    generateFile.CreateRepositoryFile();
    generateFile.CreateValidatorFile();

    //Generate UseCases
    var generateUseCaseFile = new GenerateUseCaseFile(nomeDaEntidade, folders.UseCasePath);
    generateUseCaseFile.CreateUseCaseFile();
    generateUseCaseFile.UpdatUseCaseFile();
    generateUseCaseFile.GetAllUseCaseFile();
    generateUseCaseFile.GetByIdUseCaseFile();
    generateUseCaseFile.DeleteUseCaseFile();

    //Generate Repository
    var generateRepositoryFile = new GenerateRepositoryFile(nomeDaEntidade, folders.TypeormPath, folders.TypeormSchemaPath);
    generateRepositoryFile.GenerateSchemaFile();
    generateRepositoryFile.GenerateRepository();

    Console.WriteLine("Pasta criada com sucesso");
}
catch (Exception e)
{
    Console.WriteLine(e);
}
