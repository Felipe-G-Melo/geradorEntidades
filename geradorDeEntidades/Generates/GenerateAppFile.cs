namespace geradorDeEntidades.Generates;
public class GenerateAppFile : Utils
{
    public string EntityName { get; private set; }
    public DirectoryInfo AppPath { get; private set; }
    public DirectoryInfo AppDtoPath { get; private set; }
    public List<Property> Properties { get; private set; }

    public GenerateAppFile(
        string entityName,
        DirectoryInfo appPath,
        DirectoryInfo appDtoPath,
        List<Property> properties)
    {
        EntityName = entityName;
        AppPath = appPath;
        AppDtoPath = appDtoPath;
        Properties = properties;
    }

    public void GenerateDtoFile()
    {
        string dtoFile = "create-"+EntityName + ".dto.ts";
        string pathDto = Path.Combine(AppDtoPath.FullName, dtoFile);
        using (StreamWriter sw = File.CreateText(pathDto))
        {
            sw.WriteLine($"export class Create{ClearChar(EntityName)}Dto{{");
            foreach (var property in Properties)
            {
                sw.WriteLine($"    {property.Name}: {property.Type};");
            }
            sw.WriteLine("}");
        }
    }

    public void GenerateServiceFile()
    {
        string serviceFile = EntityName + ".service.ts";
        string pathService = Path.Combine(AppPath.FullName, serviceFile);
        using (StreamWriter sw = File.CreateText(pathService))
        {
            sw.WriteLine("import { Injectable } from '@nestjs/common';" +
                $"import {{Create{ClearChar(EntityName)}Dto}} from './dto/create-{EntityName}.dto';" +
                "import { GenericService } from '../abstractions/generic.service';" +
                $"import {{{ClearChar(EntityName)}}} from '@domain/{EntityName}/entities/{EntityName}';" +
                $"import {{Create{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/create-{EntityName}';" +
                $"import {{Update{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/update-{EntityName}';" +
                $"import {{Get{ClearChar(EntityName)}ById}} from '@use-cases/{EntityName}/get-{EntityName}-by-id';" +
                $"import {{Update{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/delete-{EntityName}';" +
                $"import {{Get{ClearChar(EntityName)}}} from '@use-cases/{EntityName}/get-{EntityName}';" +
                "@Injectable()" +
                $"export class {ClearChar(EntityName)}Service extends GenericService<" +
                $" Create{ClearChar(EntityName)}Dto," +
                $" {ClearChar(EntityName)}," +
                $" Create{ClearChar(EntityName)}," +
                $" Update{ClearChar(EntityName)}," +
                $" Get{ClearChar(EntityName)}ById," +
                $" Delete{ClearChar(EntityName)}," +
                $" Get{ClearChar(EntityName)}" +
                "> {" +
                "  constructor(" +
                $"    private readonly create{ClearChar(EntityName)}: Create{ClearChar(EntityName)}," +
                $"    private readonly update{ClearChar(EntityName)}: Update{ClearChar(EntityName)}," +
                $"    private readonly get{ClearChar(EntityName)}ById: Get{ClearChar(EntityName)}ById," +
                $"    private readonly delete{ClearChar(EntityName)}: Delete{ClearChar(EntityName)}," +
                $"    private readonly get{ClearChar(EntityName)}: Get{ClearChar(EntityName)},  ) {{" +
                "    super(" +
                $"      create{ClearChar(EntityName)}," +
                $"      update{ClearChar(EntityName)}," +
                $"      get{ClearChar(EntityName)}ById," +
                $"      delete{ClearChar(EntityName)}," +
                $"      get{ClearChar(EntityName)}," +
                "    );" +
                "  }" +
                "}" +
                "");
        }
    }

    public void GenerateModuleFile()
    {
        string moduleFile = EntityName + ".module.ts";
        string pathModule = Path.Combine(AppPath.FullName, moduleFile);
        using (StreamWriter sw = File.CreateText(pathModule))
        {
            sw.WriteLine("import { Module } from '@nestjs/common';" +
                $"import {{{ClearChar(EntityName)}Service}} from './{EntityName}.service';" +
                $"import {{{ClearChar(EntityName)}Controller }} from './{EntityName}.controller';" +
                $"import {{{ClearChar(EntityName)}Provider}} from 'src/providers/{EntityName}.provider';" +
                "@Module({" +
                $"  controllers: [{ClearChar(EntityName)}Controller]," +
                $"  providers: [{ClearChar(EntityName)}Service, ...Object.values({ClearChar(EntityName)}Provider)]," +
                "})" +
                $"export class {ClearChar(EntityName)}Module {{}}");
        }
    }

    public void GenerateController()
    {
        string controllerFile = EntityName + ".controller.ts";
        string pathController = Path.Combine(AppPath.FullName, controllerFile);

        using (StreamWriter sw = File.CreateText(pathController))
        {
            sw.WriteLine("import {" +
                "  Controller," +
                "  Get," +
                "  Post," +
                "  Body," +
                "  Param," +
                "  Delete," +
                "  UseGuards," +
                "  Request," +
                "  Put," +
                " UsePipes," +
                "  ValidationPipe," +
                "  Query," +
                "} from '@nestjs/common';" +
                $"import {{{ClearChar(EntityName)}Service}} from './{EntityName}.service';" +
                $"import {{Create{ClearChar(EntityName)}Dto}} from './dto/create-{EntityName}.dto';" +
                "import { ApiQuery, ApiTags } from '@nestjs/swagger';" +
                "import { JwtGuard } from '../auth/login/jwt.guard';" +
                "@UseGuards(JwtGuard)" +
                $"@ApiTags('{ClearChar(EntityName)}')" +
                $"@Controller('{EntityName}')" +
                $"export class {ClearChar(EntityName)}Controller {{" +
                $"  constructor(private readonly {NotClearFirstChar(EntityName)}Service: {ClearChar(EntityName)}Service) {{}}" +
                "\n  @Post()" +
                $"  async Create(@Body() create{ClearChar(EntityName)}Dto: Create{ClearChar(EntityName)}Dto) {{" +
                $"    return await this.{NotClearFirstChar(EntityName)}Service.Create(create{ClearChar(EntityName)}Dto);" +
                "  }" +
                "  @ApiQuery({ name: 'page', required: false })" +
                "  @ApiQuery({ name: 'limit', required: false })" +
                "  @ApiQuery({ name: 'searchColumn', required: false })" +
                "  @ApiQuery({ name: 'searchValue', required: false })" +
                "  @ApiQuery({ name: 'orderBy', required: false })" +
                "  @ApiQuery({ name: 'orderDirection', required: false })" +
                "  @UsePipes(new ValidationPipe({ transform: true }))" +
                "  @Get()" +
                "\n async FindAll(" +
                "   @Query('page') page: number," +
                "    @Query('limit') limit: number," +
                "    @Query('searchColumn') searchColumn: string," +
                "    @Query('searchValue') searchValue: string," +
                "    @Query('orderBy') orderBy: string," +
                "    @Query('orderDirection') orderDirection: string," +
                "  ) {" +
                $"    return await this.{NotClearFirstChar(EntityName)}Service.FindAll(" +
                "      page," +
                "      limit," +
                "     searchColumn," +
                "     searchValue," +
                "      orderBy," +
                "      orderDirection," +
                "    );" +
                "  }" +
                "\n  @Get(':id')" +
                "  async FindOne(@Param('id') id: string) {" +
                $"    return await this.{NotClearFirstChar(EntityName)}Service.FindOne(+id);" +
                "  }" +
                "\n  @Put(':id')" +
                "  async Update(" +
                "    @Param('id') id: string," +
                $"    @Body() update{ClearChar(EntityName)}Dto: Create{ClearChar(EntityName)}Dto," +
                "  ) {" +
                $"    return await this.{NotClearFirstChar(EntityName)}Service.Update(update{ClearChar(EntityName)}Dto, +id);" +
                "  }" +
                "\n  @Delete(':id')" +
                "  async Remove(@Param('id') id: string) {" +
                $"    return await this.{NotClearFirstChar(EntityName)}Service.Remove(+id);" +
                "  }" +
                "}");
        }
    }
}
