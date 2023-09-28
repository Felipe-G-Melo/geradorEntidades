namespace geradorDeEntidades.Generates;
public class GenerateDomainFile
{
    public string EntityName { get; private set; }
    public DirectoryInfo DomainEntityPath { get; private set; }
    public DirectoryInfo DomainInputPath { get; private set; }
    public DirectoryInfo DomainRepositorytPath { get; private set; }
    public DirectoryInfo DomainValidatortPath { get; private set; }
    public List<Property>? Properties { get; private set; } = new List<Property>();
    public List<Property>? InputProperties { get; private set; } = new List<Property>();
    public bool Flag { get; private set; } = true;
    public int Count { get; private set; } = 0;

    public GenerateDomainFile(
        string entityName, 
        DirectoryInfo domainEntityPath,
        DirectoryInfo domainInputPath,
        DirectoryInfo domainRepositorytPath,
        DirectoryInfo domainValidatortPath
    )
    {
        EntityName = entityName;
        DomainEntityPath = domainEntityPath;
        DomainInputPath = domainInputPath;
        DomainRepositorytPath = domainRepositorytPath;
        DomainValidatortPath = domainValidatortPath;
    }

    public void SetProperties()
    {
        while (Flag)
        {
            Count++;
            Console.Write($"\nDigite o nome da propriedade {Count}: ");
            string nomeDaPropriedade = Console.ReadLine();
            Console.Write($"Digite o tipo da propriedade {Count}: ");
            string tipoDaPropriedade = Console.ReadLine();
            Properties.Add(new Property(nomeDaPropriedade, tipoDaPropriedade));
            Console.WriteLine("Deseja adicionar mais uma propriedade? (s/n)");
            string resposta = Console.ReadLine();
            if (resposta == "n")
            {
                Flag = false;
            }
        }
    }

    public void SetInputProperts()
    {
        Console.WriteLine("\nQUAIS PROPRIEDADES TERÃO NO INPUT ? ");
        foreach (var property in Properties)
        {
            Console.WriteLine($"\nA propriedade {property.Name} terá no input? (s/n)");
            string resposta = Console.ReadLine();
            if (resposta == "s")
            {
                InputProperties.Add(property);
            }
        }
    }

    public void CreateEntityFile()
    {
        string fileEntity = EntityName + ".ts";
        string pathEntity = Path.Combine(DomainEntityPath.FullName, fileEntity);
        using (StreamWriter sw = File.CreateText(pathEntity))
        {
            //IMPORTS
            sw.WriteLine("import { Basic } from '@domain/basic/basic';");
            sw.WriteLine("import { " + FirstCharToUpper(EntityName) + "Input } from '../input/" + EntityName + "-input';");
            sw.WriteLine("import HttpError from '@domain/utils/errors/http-errors';");
            sw.WriteLine($"import {EntityName}ValidatorFactory from '../validator/{EntityName}-validator';");

            //CLASS
            sw.WriteLine("\nexport class " + FirstCharToUpper(EntityName) + " extends Basic {");

            //ENTITY PROPERTIES
            foreach (var property in Properties)
            {
                sw.WriteLine("  " + property.Name + ": " + property.Type + ";");
            }

            //CONSTRUCTOR
            sw.WriteLine("\n  constructor(props:" + FirstCharToUpper(EntityName) + "Input, id?:number){");
            sw.WriteLine("      super();");
            sw.WriteLine("      Object.assign(this, props);");
            sw.WriteLine("      this.id = id;");
            sw.WriteLine("  }");

            //CREATE
            sw.WriteLine("\n  static Create(props:" + FirstCharToUpper(EntityName) + "Input, id?:number){");
            sw.WriteLine("      this.Validate(props);");
            sw.WriteLine("      return new " + FirstCharToUpper(EntityName) + "(props, id);");
            sw.WriteLine("  }");

            //VALIDATE
            sw.WriteLine("\n  static Validate(props:" + FirstCharToUpper(EntityName) + "Input){");
            sw.WriteLine("      const validator = " + FirstCharToUpper(EntityName) + "ValidatorFactory.Create()");
            sw.WriteLine("      validator.Validate(props)");
            sw.WriteLine("      if(validator.errors) {");
            sw.WriteLine("          new HttpError({ errors: validator.errors }).BadRequest();");
            sw.WriteLine("      }");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    public void CreateInputFile()
    {
        string fileInput = EntityName + "-input.ts";
        string pathInput = Path.Combine(DomainInputPath.FullName, fileInput);
        using (StreamWriter sw = File.CreateText(pathInput))
        {
            sw.WriteLine("export type " + FirstCharToUpper(EntityName) + "Input = {");
            foreach (var property in InputProperties)
            {
                sw.WriteLine("  " + property.Name + ": " + property.Type + ";");
            }
            sw.WriteLine("};");
        }
    }

    public void CreateRepositoryFile()
    {
        string fileRepository = "i" + EntityName + "-repository.ts";
        string pathRepository = Path.Combine(DomainRepositorytPath.FullName, fileRepository);

        using (StreamWriter sw = File.CreateText(pathRepository))
        {
            sw.WriteLine("/* eslint-disable @typescript-eslint/no-empty-interface */");
            sw.WriteLine("import { IRepository } from '@domain/basic/irepository';");
            sw.WriteLine("import { " + FirstCharToUpper(EntityName) + " } from '../entities/" + EntityName + "';");
            sw.WriteLine("export interface I" + FirstCharToUpper(EntityName) + "Repository extends IRepository<" + FirstCharToUpper(EntityName) + "> {");
            sw.WriteLine("}");
        }
    }

    public void CreateValidatorFile()
    {
        string fileValidator = EntityName + "-validator.ts";
        string pathValidator = Path.Combine(DomainValidatortPath.FullName, fileValidator);

        using (StreamWriter sw = File.CreateText(pathValidator))
        {
            //IMPORTS
            sw.WriteLine("import { ClassValidatorFields } from '@domain/utils/validations/class-validator-fields';");
            sw.WriteLine("import { " + FirstCharToUpper(EntityName) + "Input } from '../input/" + EntityName + "-input';");

            //CLASS
            sw.WriteLine("export class " + FirstCharToUpper(EntityName) + "Rules {");

            //ENTITY PROPERTIES
            foreach (var property in InputProperties)
            {
                sw.WriteLine("  " + property.Name + ": " + property.Type + ";");
            }

            //CONSTRUCTOR
            sw.WriteLine("  constructor(props: " + FirstCharToUpper(EntityName) + "Input) {");
            sw.WriteLine("      Object.assign(this, props);");
            sw.WriteLine("  }");
            sw.WriteLine("}");

            //VALIDATE
            sw.WriteLine("\nexport class " + FirstCharToUpper(EntityName) + "Validator extends ClassValidatorFields<" + FirstCharToUpper(EntityName) + "Rules> {");
            sw.WriteLine("  Validate(data: " + FirstCharToUpper(EntityName) + "Rules): boolean {");
            sw.WriteLine("      return super.validate(new " + FirstCharToUpper(EntityName) + "Rules(data));");
            sw.WriteLine("  }");
            sw.WriteLine("}");

            //VALIDATOR FACTORY
            sw.WriteLine("\nexport default class " + FirstCharToUpper(EntityName) + "ValidatorFactory {");
            sw.WriteLine("  static Create(){");
            sw.WriteLine("      return new " + FirstCharToUpper(EntityName) + "Validator();");
            sw.WriteLine("  }");
            sw.WriteLine("}");
        }
    }

    string FirstCharToUpper(string input)
    {
        if (String.IsNullOrEmpty(input))
            throw new ArgumentException("Insira um nome válido");
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}
