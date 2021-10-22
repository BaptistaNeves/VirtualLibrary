import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Livro } from 'src/app/_models/produto/livro';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
    baseUr = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterTodos() {
      return this.http.get<Livro>(this.baseUr + "livros");
  }

  obterPorCategoriaId(id: string) {
      return this.http.get<Livro>(this.baseUr + "livros/livros-por-categoria/" + id);
  }

  obterPorId(id:string) {
      return this.http.get<Livro>(this.baseUr + "livros/" + id);
  }

  adicionar(model: Livro) {
      return this.http.post<Livro>(this.baseUr + "livros", model);
  }

  atualizar(id: string, model: Livro) {
      return this.http.put<Livro>(this.baseUr + "livros/" + id, model);
  }

  remover(id: string) {
      return this.http.delete<Livro>(this.baseUr + "livros/" + id);
  }

}
