class Program
{
    static void Main()
    {
        // Informando o nome do arquivo manipulado
        string fileName = @"C:\git\ProjetoManipulacaoCSV\ProjetoManipulacaoCSV\Candidatos.txt";

        // Inicializando as variaveis para evitar lixo de memoria
        int vagaQA = 0, vagaMobile = 0, vagaWeb = 0, idadeQA = 0, idadeWeb = 0, idadeMobile = 0, idadeMaiorQa = 0, idadeMaiorWeb = 0, idadeMaiorMobile = 0, idadeMenorQa = 100, idadeMenorWeb = 100, idadeMenorMobile = 100;

        // Criando o Array de UFs
        List<string> UFs = new List<string>();

        // Criando um vetor de Entidade de candidatos
        Candidatos[] candidatos = new Candidatos[1000];

        Candidatos instrutorQA = new Candidatos();

        Candidatos instrutorMobile = new Candidatos();

        // Realizar o processo de ler o arquivo TXT e capturar as informacoes
        try
        {
            // Contador que controlara o indice do vetor de candidatos
            int contador = 0;

            // Realiza a leitura de todas as linhas do arquivo e armazena em um vetor de strings
            string[] lines = File.ReadAllLines(fileName);

            for (int i = 1; i < lines.Length; i++) // Comeca a partir do indice 1, pois o 0 nao possui informacoes
            {

                // Separar as informacoes a cada ';' com o split e armazena em um vetor de strings
                string[] valores = lines[i].Split(';');

                // Captura os valores em variaveis
                // Seria possivel trabalhar diretamente com o valor do vetor de valores porem em variaveis ficara mais legivel
                string nomeCandidato = valores[0];
                string idadeCandidatoStr = valores[1].Replace(" anos", "").Trim(); // Remove o texto da idade para uma conversao para int
                string vagaCandidato = valores[2];
                string ufCandidato = valores[3];

                // Verifica se na lista de UFs ja possui a UF da linhas
                if (!UFs.Contains(ufCandidato))
                {
                    UFs.Add(ufCandidato);
                }

                // Realiza a conversao da idade de string para int, caso consiga realizar a conversao criara a entidade e
                //armazenara na posicao do vetor definida pelo contador
                // Nesse exemplo estou levando em conta que os dados trazidos no TXT sao todos consistentes e nao havera erros no arquivo
                if (int.TryParse(idadeCandidatoStr, out int idadeCandidato))
                    candidatos[contador] = new Candidatos(nomeCandidato, idadeCandidato, vagaCandidato, ufCandidato);

                // Realiza a captura de informacoes de acordo com a vaga(QA, Web, Mobile)
                switch (vagaCandidato)
                {
                    case "QA":
                        vagaQA++; // ContarC! a quantidade de candidatos para QA
                        idadeQA += idadeCandidato; // Somara todas as idades dos candidatos a QA
                                                   // Encontrara o Candidato mais velho
                        if (idadeCandidato > idadeMaiorQa)
                        {
                            idadeMaiorQa = idadeCandidato;
                        }
                        // Encontrara o Candidato mais jovem
                        if (idadeCandidato < idadeMenorQa)
                        {
                            idadeMenorQa = idadeCandidato;
                        }
                        break;
                    case "Web":
                        vagaWeb++; // Contara a quantidade de candidatos para Web
                        idadeWeb += idadeCandidato; // Somara todas as idades dos candidatos a Web
                                                    // Encontrara o Candidato mais velho
                        if (idadeCandidato > idadeMaiorWeb)
                        {
                            idadeMaiorWeb = idadeCandidato;
                        }
                        // Encontrara o Candidato mais jovem
                        if (idadeCandidato < idadeMenorWeb)
                        {
                            idadeMenorWeb = idadeCandidato;
                        }
                        break;
                    case "Mobile":
                        vagaMobile++; // Contara a quantidade de candidatos para Mobile
                        idadeMobile += idadeCandidato; // Somara todas as idades dos candidatos a Mobile
                                                       // Encontrara o Candidato mais velho
                        if (idadeCandidato > idadeMaiorMobile)
                        {
                            idadeMaiorMobile = idadeCandidato;
                        }
                        // Encontrara o Candidato mais jovem
                        if (idadeCandidato < idadeMenorMobile)
                        {
                            idadeMenorMobile = idadeCandidato;
                        }
                        break;
                }
                contador++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro durante o processo: {ex}");
        }

        try
        {

            string csvPath = @"C:\Teste\ASorted_Academy_Candidates.csv";

            // Verifica se o arquivo existe
            if (File.Exists(csvPath))
            {
                Console.WriteLine(">>> O arquivo já existe! Será excluído e readicionado");

                // Limpa o arquivo existente antes de inserir novos dados
                File.Delete(csvPath);
            }

            // Cria ou sobrescreve o arquivo CSV e insere os dados
            using (StreamWriter sw = File.CreateText(csvPath))
            {

                Array.Sort(candidatos); // O array candidatos deve implementar IComparable<Candidatos>

                foreach (Candidatos candidato in candidatos)
                {
                    string candidatoNome = candidato.Nome;
                    int candidatoIdade = candidato.Idade;
                    string candidatosVaga = candidato.Vaga;
                    string candidatoUF = candidato.UF;

                    // Escreve no formato CSV
                    sw.WriteLine($"{candidatoNome};{candidatoIdade};{candidatosVaga};{candidatoUF}");
                }
            }

            Console.WriteLine(">>> CSV criado e dados inseridos com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro durante o processo: {ex}");
        }

        // Utilizado para encontrar os instrutores
        // Nesse caso, cada objeto precisará seguir uma serie de regras para ser considerado um instrutur
        foreach (Candidatos c in candidatos)
        {
            if (c.Vaga == "QA")
            {
                if (c.UF == "SC")
                {

                    // -------------------
                    if (c.Idade == 25)
                    {

                        string[] nomes = c.Nome.Split(" ");
                        string primeiroNome = nomes[0].ToLower();

                        string nomePalindromo = primeiroNome.ToLower();
                        char[] charArray = nomePalindromo.ToCharArray();
                        Array.Reverse(charArray);
                        string nomeInvertido = new string(charArray);

                        if (nomeInvertido == primeiroNome)
                        {
                            instrutorQA.Nome = c.Nome;
                            instrutorQA.Idade = c.Idade;
                            instrutorQA.Vaga = c.Vaga;
                            instrutorQA.UF = c.UF;
                        }

                    }

                    // ----------------
                }
            }
            if (c.Vaga == "Mobile")
            {
                if (c.UF == "PI")
                {
                    string[] nomes = c.Nome.Split(" ");
                    string sobrenome = nomes[nomes.Length - 1];
                    if (sobrenome[0] == 'C')
                    {
                        if (c.Idade >= 30 && c.Idade <= 40 && c.Idade % 2 == 0)
                        {
                            instrutorMobile.Nome = c.Nome;
                            instrutorMobile.Idade = c.Idade;
                            instrutorMobile.Vaga = c.Vaga;
                            instrutorMobile.UF = c.UF;
                        }
                    }
                }
            }
        }


        double porcentagemQA = (vagaQA * 100.0) / 1000;
        double porcentagemWeb = (vagaWeb * 100.0) / 1000;
        double porcentagemMobile = (vagaMobile * 100.0) / 1000;

        double idadeMediaQA = idadeQA / vagaQA;
        // double idadeMediaWeb = idadeWeb / vagaWeb;
        // double idadeMediaMobile = idadeMobile / vagaMobile;

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("QA: ", porcentagemQA, "% dos candidatos"));
        Console.WriteLine(string.Concat("Web: ", porcentagemWeb, "% dos candidatos"));
        Console.WriteLine(string.Concat("Mobile: ", porcentagemMobile, "% dos candidatos"));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Idade média dos candidatos de QA: ", idadeMediaQA));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Idade do Candidato mais velho de Mobile: ", idadeMaiorMobile));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Idade do Candidato mais jovem de Web: ", idadeMenorWeb));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Soma das idades dos Candidato de QA: ", idadeQA));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Numero de estados distintos entre os candidatos: ", UFs.Count));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Instrutor de QA: ", instrutorQA.Nome));

        Console.WriteLine("--------------");

        Console.WriteLine(string.Concat("Instrutor de Mobile: ", instrutorMobile.Nome));

    }

}

// Criacao da Entidade Candidato
public class Candidatos : IComparable<Candidatos>
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Vaga { get; set; }
    public string UF { get; set; }

    public Candidatos() { }

    public Candidatos(string nome, int idade, string vaga, string uf)
    {
        Nome = nome;
        Idade = idade;
        Vaga = vaga;
        UF = uf;
    }

    public override string ToString()
    {
        return $"Nome: {Nome}, Idade: {Idade}, Vaga: {Vaga}, UF: {UF}";
    }

    public int CompareTo(Candidatos outroCandidato)
    {
        // Ordenar pelo nome em ordem alfabetica
        return this.Nome.CompareTo(outroCandidato.Nome);
    }
}