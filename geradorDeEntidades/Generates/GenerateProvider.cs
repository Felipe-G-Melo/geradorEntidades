namespace geradorDeEntidades.Generates;
public class GenerateProvider
{
    public string EntityName { get; private set; }
    public DirectoryInfo ProvidePath { get; private set; }

    public GenerateProvider(
        string entityName,
        DirectoryInfo providePath)
    {
        EntityName = entityName;
        ProvidePath = providePath;
    }

    public void CreateProviderFile()
    {
        string providerName = $"{EntityName}.provider.ts";
        string providerPath = Path.Combine(ProvidePath.FullName, providerName);

        using (StreamWriter sw = File.CreateText(providerPath))
        {
            sw.WriteLine("" +
                $"import {{{FirstCharToUpper(EntityName)}TypeormRepository}} from '@db/repositories/typeorm/{EntityName}/{EntityName}-typeorm.repository';" +
                $"import {{{FirstCharToUpper(EntityName)}}} from '@domain/{EntityName}/entities/{EntityName}';" +
                $"import {{I{EntityName}Repository}} from '@domain/{EntityName}/repository/i{EntityName}-repository';" +
                "import { Provider } from '@nestjs/common';" +
                "import { getDataSourceToken } from '@nestjs/typeorm';" +
                $"import {{Create{FirstCharToUpper(EntityName)}}} from '@use-cases/{EntityName}/create-{EntityName}';" +
                $"import {{Delete{FirstCharToUpper(EntityName)}}} from '@use-cases/{EntityName}/delete-{EntityName}';" +
                $"import {{Get{FirstCharToUpper(EntityName)}}} from '@use-cases/{EntityName}/get-{EntityName}';" +
                $"import {{Get{FirstCharToUpper(EntityName)}ById}} from '@use-cases/{EntityName}/get-{EntityName}-by-id';" +
                $"import {{Update{FirstCharToUpper(EntityName)}}} from '@use-cases/{EntityName}/update-{EntityName}';" +
                "import { DataSource } from 'typeorm';" +
                $"\nexport const {EntityName}Provider: Provider[] = [" +
                "  {" +
                $"   provide: {FirstCharToUpper(EntityName)}TypeormRepository," +
                "" +
                "    useFactory: (dataSource: DataSource) =>" +
                $"      new {FirstCharToUpper(EntityName)}TypeormRepository(dataSource.getRepository({FirstCharToUpper(EntityName)}))," +
                "    inject: [getDataSourceToken()]," +
                "  }," +
                "  {" +
                $"    provide: Create{FirstCharToUpper(EntityName)}," +
                $"    useFactory: (repo{FirstCharToUpper(EntityName)}: I{FirstCharToUpper(EntityName)}Repository) =>" +
                $"      new Create{FirstCharToUpper(EntityName)}(repo{FirstCharToUpper(EntityName)})," +
                $"    inject: [{FirstCharToUpper(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Update{FirstCharToUpper(EntityName)}," +
                $"   useFactory: (repo{FirstCharToUpper(EntityName)}: I{FirstCharToUpper(EntityName)}Repository) =>" +
                $"      new Update{FirstCharToUpper(EntityName)}(repo{FirstCharToUpper(EntityName)})," +
                $"    inject: [{FirstCharToUpper(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Get{FirstCharToUpper(EntityName)}ById," +
                $"    useFactory: (repo{FirstCharToUpper(EntityName)}: I{FirstCharToUpper(EntityName)}Repository) => new Get{FirstCharToUpper(EntityName)}ById(repo{FirstCharToUpper(EntityName)})," +
                $"    inject: [{FirstCharToUpper(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Delete{FirstCharToUpper(EntityName)}," +
                $"    useFactory: (repo{FirstCharToUpper(EntityName)}: I{FirstCharToUpper(EntityName)}Repository) => new Delete{FirstCharToUpper(EntityName)}(repo{FirstCharToUpper(EntityName)})," +
                $"    inject: [{FirstCharToUpper(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Get{FirstCharToUpper(EntityName)}," +
                $"   useFactory: (repo{FirstCharToUpper(EntityName)}: I{FirstCharToUpper(EntityName)}Repository) => new Get{FirstCharToUpper(EntityName)}(repo{FirstCharToUpper(EntityName)})," +
                $"    inject: [{FirstCharToUpper(EntityName)}TypeormRepository]," +
                "  }," +
                "];" +
                "");
        }

        string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("Insira um nome válido");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
