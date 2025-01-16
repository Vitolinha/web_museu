using web_museu.Data;
using web_museu.Models;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly BancoContext _context;

    public UsuarioRepositorio(BancoContext context)
    {
        _context = context;
    }

    public List<UsuarioModel> BuscarTodos()
    {
        return _context.Usuarios.ToList();
    }
    public UsuarioModel BuscarPorLogin(string login)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper());
    }
    public UsuarioModel BuscarPorEmailELogin(string email, string login)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email.ToUpper() == email.ToUpper() && u.Login.ToUpper() == login.ToUpper());
    }
    public UsuarioModel BuscarPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Id == id);
    }

    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        usuario.DataCadastro = DateTime.Now;
        usuario.SetSenhaHash();
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }

    public UsuarioModel Atualizar(UsuarioModel usuario)
    {
        // Busca o usuário no banco de dados pelo ID
        UsuarioModel usuarioDB = BuscarPorId(usuario.Id);

        // Verifica se o usuário existe
        if (usuarioDB == null)
            throw new Exception("Houve um erro na atualização do usuário!");

        // Atualiza os campos
        usuarioDB.Nome = usuario.Nome;
        usuarioDB.Email = usuario.Email;
        usuarioDB.Login = usuario.Login;
        usuarioDB.Perfil = usuario.Perfil;
        usuarioDB.DataAtualizacao = DateTime.Now; // Sem acento

        // Atualiza no contexto e salva as mudanças
        _context.Usuarios.Update(usuarioDB);
        _context.SaveChanges();

        return usuarioDB;
    }

    public bool Apagar(int id)
    {
        // Busca o usuário pelo ID
        UsuarioModel usuarioDB = BuscarPorId(id);

        // Verifica se o usuário foi encontrado
        if (usuarioDB == null)
            throw new Exception("Houve um erro na deleção do usuário!");

        // Remove o usuário do banco de dados
        _context.Usuarios.Remove(usuarioDB);
        _context.SaveChanges();

        return true;
    }

    
}
