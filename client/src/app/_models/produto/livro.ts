import { Autor } from "../autor";
import { Editora } from "../editora";
import { Fornecedor } from "../fornecedor";
import { Categoria } from "./categoria";
import { Usuario } from "./usuario";

export interface Livro {
    id: string;
    titulo: string;
    imagem: string;
    livroImagem: string;
    descricao: string;
    categoriaId: string;
    autorId: string;
    editoraId: string;
    fornecedorId: string;
    estado: boolean;
    dataCadastro: Date;
    dataAtualizacao: Date;

    categoria: Categoria;
    autor: Autor;
    fornecedor: Fornecedor;
    editor: Editora;
    usuario: Usuario;
}