import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoriaService } from '../_services/produto/categoria.service';
import { LivroService } from '../_services/produto/livro.service';

@Component({
  selector: 'app-livros',
  templateUrl: './livros.component.html',
  styleUrls: ['./livros.component.css']
})
export class LivrosComponent implements OnInit {
  livros: any;
  categorias: any;
  livroForm: FormGroup;

  constructor(private livroService: LivroService, 
              private categoriaService:CategoriaService,
              private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.listarLivros();
    this.initializeForm();
  }

  listarLivros() {
    this.livroService.obterTodos().subscribe(response => {
      this.livros = response;
    })
  }

  listarCategorias() {
    this.categoriaService.obterCategorias().subscribe(response => {
      this.categorias = response;
    })
  }

  listarPorCategoriaId(id: string) {
    this.livroService.obterPorCategoriaId(id).subscribe(response => {
      this.livros = response;
    })
  }

  initializeForm() {
    this.livroForm = this.formBuilder.group({
      titulo: ['', [Validators.required, Validators.minLength(4)]],
      livroImagem: ['', [Validators.required]],
      descricao: ['', [Validators.maxLength(1000)]],
      categoriaId: ['', [Validators.required]],
      autorId: ['', [Validators.required]],
      fornecedorId: ['', [Validators.required]],
      editorId: ['', [Validators.required]]
    })
  }

  get f() {return this.livroForm.controls}

  adicionar() {
    
  }
}
