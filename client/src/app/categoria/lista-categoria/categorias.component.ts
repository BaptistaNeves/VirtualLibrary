import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Categoria } from 'src/app/_models/produto/categoria';
import { CategoriaService } from 'src/app/_services/produto/categoria.service';

@Component({
  selector: 'app-lista-categoria',
  templateUrl: './categorias.component.html'
})
export class CategoriasComponent implements OnInit {
  adicionarForm: FormGroup;
  editarForm: FormGroup;
  categorias: any;
  categoria: any;
  modelStateErrors: any;

  constructor(private formBuilder: FormBuilder,
              private categoriaService: CategoriaService, 
              private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.listarCategoria();
    this.initializerAddForm();
    this.initializeEditForm();
  }

  initializerAddForm() {
    this.adicionarForm = this.formBuilder.group({
      descricao: ["", [Validators.required, Validators.minLength(2)]]
    });
  }

  initializeEditForm() {
    this.editarForm = this.formBuilder.group({
      descricao: ["", [Validators.required, Validators.minLength(2)]]
    });
  }

  get f() {return this.adicionarForm.controls;}
  get e() {return this.editarForm.controls;}

  listarCategoria() {
    this.categoriaService.obterCategorias().subscribe(response => {
      this.categorias = response;
      console.log(response);
    })
  }

  adicionar() {
    this.categoriaService.adicionar(this.adicionarForm.value).subscribe(response => {
          let fechar: HTMLElement = document.getElementById('add_fechar') as HTMLElement;
          fechar.click();
          this.listarCategoria();
          this.limparCampo();
          this.toastr.success("Categoria adicionada com sucesso!");
    }, error => {
      this.modelStateErrors = error;
    })
  }

  detalhes(categoria: any) {
    this.categoria = categoria;
  }

  editar(categoria: any) {
    this.categoria = categoria;
    this.editarForm.controls['descricao'].setValue(categoria.descricao);
  }

  atualizar() {
    this.categoriaService.atualizar(this.categoria.id, this.editarForm.value)
    .subscribe(response => {
      let fechar: HTMLElement = document.getElementById('editar_fechar') as HTMLElement;
      fechar.click();
      this.listarCategoria();
      this.limparCampo();
      this.toastr.success("Categoria Atualizada com sucesso!");
      console.log(response);
    },error => {
      this.modelStateErrors = error;
    })
  }

  confirmaEliminar(categoria: Categoria) {
    this.categoria = categoria;
  }

  eliminar() {
    this.categoriaService.remover(this.categoria.id).subscribe(response => {
      let fechar: HTMLElement = document.getElementById('eliminar_fechar') as HTMLElement;
            fechar.click();
            this.listarCategoria();
            this.limparCampo();
            this.toastr.success("Categoria Eliminada com sucesso!");
    }, error => {
      this.modelStateErrors = error;
    })
  }

  limparCampo() {
    this.adicionarForm.reset();
  }

}
