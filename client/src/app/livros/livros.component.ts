import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Livro } from '../_models/produto/livro';
import { AutorService } from '../_services/autor.service';
import { EditoraService } from '../_services/editora.service';
import { FornecedorService } from '../_services/fornecedor.service';
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
  autores: any;
  editoras: any;
  fornecedores: any;
  livroForm: FormGroup;
  modelStateErrors: any;
  imgSrc: string = '';

  constructor(private livroService: LivroService, 
              private categoriaService:CategoriaService,
              private autorService: AutorService,
              private editoraService: EditoraService,
              private fornecedorService: FornecedorService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.listarLivros();
    this.listarCategorias();
    this.listarAutores();
    this.listarEditoras();
    this.listarFornecedores();
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

  listarAutores() {
    this.autorService.obterTodos().subscribe(response => {
      this.autores = response;
    })
  }

  listarEditoras() {
    this.editoraService.obterTodos().subscribe(response => {
      this.editoras = response;
    })
  }

  listarFornecedores() {
    this.fornecedorService.obterTodos().subscribe(response => {
      this.fornecedores = response;
    })
  }

  listarPorCategoriaId(id: string) {
    this.livroService.obterPorCategoriaId(id).subscribe(response => {
      this.livros = response;
    })
  }

  limparCampos() {
    this.livroForm.reset();
  }

  fecharModal(btn:string) {
    let fecharModalBtn: HTMLElement = document.getElementById(btn) as HTMLElement;
    fecharModalBtn.click();
  }

  initializeForm() {
    this.livroForm = this.formBuilder.group({
      titulo: ['', [Validators.required, Validators.minLength(4)]],
      imagemLivro: ['', [Validators.required]],
      descricao: ['', [Validators.maxLength(1000)]],
      categoriaId: ['', [Validators.required]],
      autorId: ['', [Validators.required]],
      fornecedorId: ['', [Validators.required]],
      editoraId: ['', [Validators.required]]
    })
  }

  get f() {return this.livroForm.controls}

  upload(event: any) {
    let file = event.target.files;
    console.log(event);
    const formData = new FormData()

    formData.append('file', file);

    this.livroForm.patchValue({
      imagemLivro: formData
    })
  }

  adicionar() {
    this.livroService.adicionar(this.livroForm.value).subscribe(response => {
      this.fecharModal('add_fechar');
      this.listarLivros();
      this.limparCampos();
      this.toastr.success("Livro Adicionado com sucesso!");
    }, errors => {
      this.modelStateErrors = errors;
      console.log(errors);
    })
  }

  adicionarForm() {
    const formData = new FormData();

    for(const key of Object.keys(this.livroForm.value)) {
      const value = this.livroForm.value[key];
      formData.append(key, value);
    }

    this.livroService.adicionar(formData).subscribe(response => {
      this.fecharModal('add_fechar');
      this.listarLivros();
      this.limparCampos();
      this.toastr.success("Livro Adicionado com sucesso!");
    }, errors => {
      this.modelStateErrors = errors;
      console.log(errors);
    })
  }

  onFileChanges(event:any) {
    if(event.target.files.length > 0 ){
      const file = event.target.files[0];
      this.livroForm.patchValue({
        imagemLivro: file
      })
    }
  }

}
