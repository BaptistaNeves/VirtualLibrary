import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Autor } from '../_models/autor';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterTodos() {
    return this.http.get<Autor>(this.baseUrl + "autores");
  }

  obterPorId(id:string) {
    return this.http.get<Autor>(this.baseUrl + "autores/" + id);
  }

  adicionar(autor: Autor) {
    return this.http.post<Autor>(this.baseUrl + "autores", autor);
  }

  atualizar(id:string, autor:Autor) {
    return this.http.put<Autor>(this.baseUrl + "autores/" + id, autor);
  }

  remover(id:string) {
    return this.http.delete(this.baseUrl + "autores/" + id);
  }
}
