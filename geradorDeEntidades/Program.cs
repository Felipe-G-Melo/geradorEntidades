using geradorDeEntidades.Generates;

Console.Write("DIGITE O NOME DA ENTIDADE: ");
string nomeDaEntidade = Console.ReadLine();

try
{
    //Generate Domain
    var folders = new GenerateInitalFolders(nomeDaEntidade);
    var generateDomainFile = new GenerateDomainFile(
        nomeDaEntidade,
        folders.DomainEntityPath,
        folders.DomainInputPath,
        folders.DomainRepositoryPath,
        folders.DomainValidatorPath
        );
    generateDomainFile.SetProperties();
    generateDomainFile.SetInputProperts();
    generateDomainFile.CreateEntityFile();
    generateDomainFile.CreateInputFile();
    generateDomainFile.CreateRepositoryFile();
    generateDomainFile.CreateValidatorFile();

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

    //Generate Provider
    var generateProvider = new GenerateProvider(nomeDaEntidade, folders.ProviderPath);
    generateProvider.CreateProviderFile();

    //Generate App
    var generateAppFile = new GenerateAppFile(nomeDaEntidade, folders.AppPath, folders.AppDtoPath, generateDomainFile.InputProperties);
    generateAppFile.GenerateDtoFile();
    generateAppFile.GenerateServiceFile();
    generateAppFile.GenerateModuleFile();
    generateAppFile.GenerateController();

    Console.WriteLine("Pasta criada com sucesso");
}
catch (Exception e)
{
    Console.WriteLine(e);
}
