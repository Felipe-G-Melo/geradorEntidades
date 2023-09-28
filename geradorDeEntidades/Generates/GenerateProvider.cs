namespace geradorDeEntidades.Generates;
public class GenerateProvider : Utils
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
                $"import {{{ClearChar(EntityName)}TypeormRepository}} from '@db/repositories/typeorm/{EntityName}/{EntityName}-typeorm.repository';" +
                $"import {{{ClearChar(EntityName)}}} from '@domain/{EntityName}/entities/{EntityName}';" +
                $"import {{I{ClearChar(EntityName)}Repository}} from '@domain/{EntityName}/repository/i{EntityName}.repository';" +
                "import { Provider } from '@nestjs/common';" +
                "import { getDataSourceToken } from '@nestjs/typeorm';" +
                $"import {{Create{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/create-{EntityName}';" +
                $"import {{Delete{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/delete-{EntityName}';" +
                $"import {{Get{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/get-{EntityName}';" +
                $"import {{Get{ClearChar(EntityName)}ById}} from '@use-cases/{EntityName}/get-{EntityName}-by-id';" +
                $"import {{Update{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/update-{EntityName}';" +
                "import { DataSource } from 'typeorm';" +
                $"\nexport const {NotClearFirstChar(EntityName)}Provider: Provider[] = [" +
                "  {" +
                $"   provide: {ClearChar(EntityName)}TypeormRepository," +
                "" +
                "    useFactory: (dataSource: DataSource) =>" +
                $"      new {ClearChar(EntityName)}TypeormRepository(dataSource.getRepository({ClearChar(EntityName)}))," +
                "    inject: [getDataSourceToken()]," +
                "  }," +
                "  {" +
                $"    provide: Create{ClearChar(EntityName)}," +
                $"    useFactory: (repo{ClearChar(EntityName)}: I{ClearChar(EntityName)}Repository) =>" +
                $"      new Create{ClearChar(EntityName)}(repo{ClearChar(EntityName)})," +
                $"    inject: [{ClearChar(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Update{ClearChar(EntityName)}," +
                $"   useFactory: (repo{ClearChar(EntityName)}: I{ClearChar(EntityName)}Repository) =>" +
                $"      new Update{ClearChar(EntityName)}(repo{ClearChar(EntityName)})," +
                $"    inject: [{ClearChar(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Get{ClearChar(EntityName)}ById," +
                $"    useFactory: (repo{ClearChar(EntityName)}: I{ClearChar(EntityName)}Repository) => new Get{ClearChar(EntityName)}ById(repo{ClearChar(EntityName)})," +
                $"    inject: [{ClearChar(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Delete{ClearChar(EntityName)}," +
                $"    useFactory: (repo{ClearChar(EntityName)}: I{ClearChar(EntityName)}Repository) => new Delete{ClearChar(EntityName)}(repo{ClearChar(EntityName)})," +
                $"    inject: [{ClearChar(EntityName)}TypeormRepository]," +
                "  }," +
                "  {" +
                $"    provide: Get{ClearChar(EntityName)}," +
                $"   useFactory: (repo{ClearChar(EntityName)}: I{ClearChar(EntityName)}Repository) => new Get{ClearChar(EntityName)}(repo{ClearChar(EntityName)})," +
                $"    inject: [{ClearChar(EntityName)}TypeormRepository]," +
                "  }," +
                "];" +
                "");
        }
    }
}
