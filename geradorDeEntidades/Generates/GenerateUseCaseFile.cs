namespace geradorDeEntidades.Generates;
public class GenerateUseCaseFile : Utils
{
    public string EntityName { get; private set; }
    public DirectoryInfo UseCasePath { get; private set; }

    public GenerateUseCaseFile(
        string entityName,
        DirectoryInfo useCasePath)
    {
        EntityName = entityName;
        UseCasePath = useCasePath;
    }

    public void CreateUseCaseFile()
    {
        string useCaseName = $"create-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);
        using(StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ {ClearChar(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ {ClearChar(EntityName)}Input }} from '@domain/{EntityName}/input/{EntityName}-input';");
            sw.WriteLine($"import {{ I{ClearChar(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}.repository';");
            sw.WriteLine($"import {{ ICreateExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Create{ClearChar(EntityName)} implements ICreateExecute<{ClearChar(EntityName)}Input, {ClearChar(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {NotClearFirstChar(EntityName)}Repository: I{ClearChar(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(input: {ClearChar(EntityName)}Input): Promise<{ClearChar(EntityName)}> {{");
            sw.WriteLine($"         const {NotClearFirstChar(EntityName)} = {ClearChar(EntityName)}.Create(input);");
            sw.WriteLine($"         return await this.{NotClearFirstChar(EntityName)}Repository.Create({NotClearFirstChar(EntityName)});");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void UpdatUseCaseFile()
    {
        string useCaseName = $"update-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);
        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ {ClearChar(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ {ClearChar(EntityName)}Input }} from '@domain/{EntityName}/input/{EntityName}-input';");
            sw.WriteLine($"import {{ I{ClearChar(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}.repository';");
            sw.WriteLine($"import {{ IUpdateExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Update{ClearChar(EntityName)} implements IUpdateExecute<{ClearChar(EntityName)}Input, {ClearChar(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {NotClearFirstChar(EntityName)}Repository: I{ClearChar(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(input: {ClearChar(EntityName)}Input, id:number): Promise<{ClearChar(EntityName)}> {{");
            sw.WriteLine($"         await this.{NotClearFirstChar(EntityName)}Repository.GetById(id);");
            sw.WriteLine($"         const {NotClearFirstChar(EntityName)} = {ClearChar(EntityName)}.Create(input, id);");
            sw.WriteLine($"         return await this.{NotClearFirstChar(EntityName)}Repository.Update({NotClearFirstChar(EntityName)});");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void GetAllUseCaseFile()
    {
        string useCaseName = $"get-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);

        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ FilterObject, OrderObject }} from '@domain/basic/irepository';");
            sw.WriteLine($"import {{ {ClearChar(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ I{ClearChar(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}.repository';");
            sw.WriteLine($"import {{ IGetAllExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Get{ClearChar(EntityName)} implements IGetAllExecute<{ClearChar(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {NotClearFirstChar(EntityName)}Repository: I{ClearChar(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(page: number, recordsPerPage: number, filter?: FilterObject[], order?: OrderObject): Promise<{{total: number; data: {ClearChar(EntityName)}[] }}> {{");
            sw.WriteLine($"         return await this.{NotClearFirstChar(EntityName)}Repository.GetAll(page, recordsPerPage, filter, order);");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void GetByIdUseCaseFile()
    {
        string useCaseName = $"get-{EntityName}-by-id.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);

        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ {ClearChar(EntityName)} }} from '@domain/{EntityName}/entities/{EntityName}';");
            sw.WriteLine($"import {{ I{ClearChar(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}.repository';");
            sw.WriteLine($"import {{ IGetByIdExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Get{ClearChar(EntityName)}ById implements IGetByIdExecute<{ClearChar(EntityName)}> {{");
            sw.WriteLine($"  constructor(private readonly {NotClearFirstChar(EntityName)}Repository: I{ClearChar(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(id:number): Promise<{ClearChar(EntityName)}> {{");
            sw.WriteLine($"         return await this.{NotClearFirstChar(EntityName)}Repository.GetById(id);");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void DeleteUseCaseFile()
    {
        string useCaseName = $"delete-{EntityName}.ts";
        string useCasePath = Path.Combine(UseCasePath.FullName, useCaseName);

        using (StreamWriter sw = File.CreateText(useCasePath))
        {
            sw.WriteLine($"import {{ I{ClearChar(EntityName)}Repository }} from '@domain/{EntityName}/repository/i{EntityName}.repository';");
            sw.WriteLine($"import {{ IDeleteExecute }} from '@use-cases/abstractions/iexecute';");
            sw.WriteLine($"\nexport class Delete{ClearChar(EntityName)} implements IDeleteExecute {{");
            sw.WriteLine($"  constructor(private readonly {NotClearFirstChar(EntityName)}Repository: I{ClearChar(EntityName)}Repository) {{ }}");
            sw.WriteLine($"\n  async Execute(id:number): Promise<void> {{");
            sw.WriteLine($"         await this.{NotClearFirstChar(EntityName)}Repository.GetById(id);");
            sw.WriteLine($"         await this.{NotClearFirstChar(EntityName)}Repository.Remove(id);");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }
}
