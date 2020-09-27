using System;
using System.Collections.Generic;

// Interface para ser implementar o Subject na classe que enviará as notificações
public interface IMySubject
{
    void Subscribe(Pessoa pessoa);
    void Unsubscribe(Pessoa pessoa);
    void Notify(string valor);
}

// Interface para a classe Observable receber
public interface IMyObservable
{
    void Update(string valor);
}

// Classe exemplo com Subject
public class YoutubeChannel : IMySubject
{
    public string Nome { get; set; }
    public List<Pessoa> pessoas;

    public YoutubeChannel(string nome)
    {
        Nome = nome;
        pessoas = new List<Pessoa>();
    }

    public void Subscribe(Pessoa pessoa)
    {
        Console.WriteLine("Usuário(a) {0} foi inscrito(a) para receber as notificações do Canal {1}", pessoa.Nome, Nome);
        pessoas.Add(pessoa);
    }

    public void Unsubscribe(Pessoa pessoa)
    {   
        Console.WriteLine("Usuário(a) {0} foi removido(a) da inscrição do Canal {1}", pessoa.Nome, Nome);
        pessoas.Remove(pessoa);
    }

    public void Notify(string valor)
    {
        foreach (var pessoa in pessoas)
        {
            pessoa.Update(valor);
        }
    }

}

// Classe exemplo com o Observable
public class Pessoa : IMyObservable
{
    public string Nome { get; set; }
    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public void Update(string valor)
    {
        Console.WriteLine("Usuário(a) {0} recebeu a notificação: {1}", Nome, valor);
    }
}

// Programa
namespace ExemploObservable
{

    class Program
    {
        static void Main(string[] args)
        {
            YoutubeChannel channel = new YoutubeChannel("Saulo canal");
            Pessoa juliana = new Pessoa("Julina");
            Pessoa aline = new Pessoa("Aline");

            channel.Subscribe(juliana);
            channel.Subscribe(aline);

            channel.Notify("Enviando notificação 1");

            channel.Unsubscribe(aline);

            channel.Notify("Enviando notificação 2");
        }
    }


}
