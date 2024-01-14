using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace controle_financeiro
{
    class Global
    {
    }
    public static class IdUsr
    {
        private static int usr_ID = 0;
        public static int ValorIdUsr
        {
            get { return usr_ID; }
            set { usr_ID = value; }
        }
    }

    public static class ID
    {
        private static int m_ID = 0;
        public static int ValorID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
    }

    public static class TipoDespesa
    {
        private static string m_tipoDespesa = "";
        public static string ValorTipoDespesa
        {
            get { return m_tipoDespesa; }
            set { m_tipoDespesa = value; }
        }
    }
    public static class DataDespesa
    {
        private static string m_dataDespesa = "";
        public static string ValorDataDespesa
        {
            get { return m_dataDespesa; }
            set { m_dataDespesa = value; }
        }
    }
    public static class Valor
    {
        private static string m_valor = "";
        public static string ValorValor
        {
            get { return m_valor; }
            set { m_valor = value; }
        }
    }
    public static class Descricao
    {
        private static string m_descricao = "";
        public static string ValorDescricao
        {
            get { return m_descricao; }
            set { m_descricao = value; }
        }
    }
    public static class Anexo
    {
        private static string m_anexo = "";
        public static string ValorAnexo
        {
            get { return m_anexo; }
            set { m_anexo = value; }
        }
    }


    public static class Nome
    {
        private static string m_nome = "";
        public static string ValorNome
        {
            get { return m_nome; }
            set { m_nome = value; }
        }
    }

    public static class Email
    {
        private static string m_email = "";
        public static string ValorEmail
        {
            get { return m_email; }
            set { m_email = value; }
        }
    }

    public static class Senha
    {
        private static string m_senha = "";
        public static string ValorSenha
        {
            get { return m_senha; }
            set { m_senha = value; }
        }
    }

    public static class Matricula
    {
        private static string m_matricula = "";
        public static string ValorMatricula
        {
            get { return m_matricula; }
            set { m_matricula = value; }
        }
    }

    public static class Saldo
    {
        private static string m_saldo = "";
        public static string ValorSaldo
        {
            get { return m_saldo; }
            set { m_saldo = value; }
        }

    }

    public static class Cargo
    {
        private static string m_cargo = "";
        public static string ValorCargo
        {
            get { return m_cargo; }
            set { m_cargo = value; }
        }
    }
        
}
