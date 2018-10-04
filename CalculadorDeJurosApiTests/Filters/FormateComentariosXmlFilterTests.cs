using Xunit;
using CalculadorDeJurosApi.Filters;
using CalculadorDeJurosApiTests.Helpers;

namespace CalculadorDeJurosApiTests.Filters
{
    public class FormateComentariosXmlFilterTEsts 
    {
        private const string _textoSemFormatacao = "Exemplo de requisição:\r\n<!-- \r\n<pre>\r\n    Isso \"aqui\" é um texto de teste\r\n    {\r\n        deve manter a formatação e remover as tags esperadas.\r\n    }\r\n</pre>\r\n-->";
        private const string _textoFormatado = "Exemplo de requisição:\r\n \r\n<pre>\rIsso \"aqui\" é um texto de teste\r{\r    deve manter a formatação e remover as tags esperadas.\r}\r</pre>\r\n";
     

        [Theory]        
        [InlineData(_textoSemFormatacao, _textoFormatado)]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void Apply_1_deve_formatar_a_descricao_e_o_resumo_quando_possuir_texto_pre_formatado(string textoSemFormatacao, string textoFormatado)
        {
            var operacao = TestHelper.GetNewOperation(textoSemFormatacao, textoSemFormatacao);            
            var filtro = new FormateComentariosXmlFilter();
            
            filtro.Apply(operacao, null, null);

            Assert.Equal(textoFormatado, operacao.Summary);
            Assert.Equal(textoFormatado, operacao.Description);
        }

        [Fact]
        public void Apply_1_deve_setar_como_required_parametros_com_tag_required()
        {
            var operacao = TestHelper.GetNewOperation(_textoSemFormatacao, _textoSemFormatacao);            
            var filtro = new FormateComentariosXmlFilter();

            filtro.Apply(operacao, null, null);

            Assert.Equal(true, operacao.Parameters[0].Required);
            Assert.Equal(false, operacao.Parameters[1].Required);
        }

        [Theory]        
        [InlineData(_textoSemFormatacao, _textoFormatado)]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void Apply_2_deve_formatar_a_descricao_e_o_resumo_quando_possuir_texto_pre_formatado(string textoSemFormatacao, string textoFormatado)
        {
            var operacao = TestHelper.GetNewOperation(textoSemFormatacao, textoSemFormatacao);            
            var filtro = new FormateComentariosXmlFilter();

            filtro.Apply(operacao, null);

            Assert.Equal(textoFormatado, operacao.Summary);
            Assert.Equal(textoFormatado, operacao.Description);
        }

        [Fact]
        public void Apply_2_deve_setar_como_required_parametros_com_tag_required()
        {
            var operacao = TestHelper.GetNewOperation(_textoSemFormatacao, _textoSemFormatacao);            
            var filtro = new FormateComentariosXmlFilter();

            filtro.Apply(operacao, null);

            Assert.Equal(true, operacao.Parameters[0].Required);
            Assert.Equal(false, operacao.Parameters[1].Required);
        }        
    }
}