import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Autor } from '../_models/autor';
import { AutorService } from '../_services/autor.service';

@Component({
  selector: 'app-autores',
  templateUrl: './autores.component.html',
  styleUrls: ['./autores.component.css']
})
export class AutoresComponent implements OnInit {
  autores: any;
  autorForm: FormGroup;
  modelStateErrors: any;
  mostrarAdd: boolean;
  mostrarAtualizar: boolean;
  autor: Autor;
  id: string;

  constructor(private autorService: AutorService,
              private formBuilder: FormBuilder, 
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.listarAutores();
    this.initializeForm();
  }

  initializeForm() {
    this.autorForm = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(2)]],
      nacionalidade: ['', [Validators.required, Validators.minLength(2)]],
      descricao: ['', Validators.maxLength(1000)]
    })
  }

  get f(){return this.autorForm.controls;}

  listarAutores() {
    this.autorService.obterTodos().subscribe(response => {
      this.autores = response;
    })
  }

  clicarAdd() {
    this.limparCampos();
    this.mostrarAdd = true;
    this.mostrarAtualizar = false;
  }

  clicarAtualizar(autor: Autor) {
    this.preencherCampos(autor);
    this.mostrarAtualizar = true;
    this.mostrarAdd = false;
  }

  clicarDetalhes(autor: Autor) {
    this.autor = autor;
  }

  clicarRemover(autor: Autor) {
    this.autor = autor;
  }

  limparCampos() {
    this.autorForm.reset();
  }

  preencherCampos(autor: Autor) {
    this.autor = autor;
    this.autorForm.controls['nome'].setValue(autor.nome);
    this.autorForm.controls['nacionalidade'].setValue(autor.nacionalidade);
    this.autorForm.controls['descricao'].setValue(autor.descricao);
  }

  fecharModal(nomeBotao: string) {
    let modal_fechar: HTMLElement = document
                    .getElementById(nomeBotao) as HTMLElement;
    modal_fechar.click();
  }

  adicionar() {
    this.autorService.adicionar(this.autorForm.value).subscribe(response => {
      this.fecharModal('modal_fechar');
      this.listarAutores();
      this.toastr.success("Autor adicionada com sucesso!");
      this.limparCampos();
    }, error => {
      this.modelStateErrors = error;
    })
  }

  atualizar() {
    this.autorService.atualizar(this.autor.id, this.autorForm.value).subscribe(response => {
      this.fecharModal('modal_fechar');
      this.listarAutores();
      this.toastr.success("Autor atualizado com sucesso!");
      this.limparCampos();
    }, error => {
      this.modelStateErrors = error;
    })
  }

  remover() {
    this.autorService.remover(this.autor.id).subscribe(response => {
      this.fecharModal('eliminar_fechar');
      this.listarAutores();
      this.toastr.success("Autor Removido com sucesso!");
    }, error => {
      this.modelStateErrors = error;
    })
  }

}
