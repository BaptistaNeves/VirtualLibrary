import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Categoria } from 'src/app/_models/produto/categoria';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterCategorias() {
    return this.http.get<Categoria>(this.baseUrl + "categorias");
  }

  obterCategoriaPorId(id: string) {
    return this.http.get<Categoria>(this.baseUrl + "categorias/"  + id);
  }

  adicionar(categoria: Categoria) {
    return this.http.post<Categoria>(this.baseUrl + "categorias", categoria);
  }

  atualizar(id: string, categoria: any) {
    return this.http.put<Categoria>(this.baseUrl + "categorias/" + id, categoria);
  }

  remover(id: string) {
    return this.http.delete(this.baseUrl + "categorias/" + id);
  }

}
