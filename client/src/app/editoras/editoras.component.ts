import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Editora } from '../_models/editora';
import { EditoraService } from '../_services/editora.service';

@Component({
  selector: 'app-editoras',
  templateUrl: './editoras.component.html',
  styleUrls: ['./editoras.component.css']
})
export class EditorasComponent implements OnInit {
  editoras: any;
  editora: Editora
  modelStateErrors: any;
  editoraForm: FormGroup;
  mostrarBtnAdd: boolean;
  mostrarBtnAtualizar: boolean;

  constructor(private editoraService: EditoraService, 
              private toastr: ToastrService, 
              private formBuild: FormBuilder) { }

  ngOnInit(): void {
    this.listaEditoras();
    this.initializeForm();
  }

  listaEditoras() {
    this.editoraService.obterTodos().subscribe(response => {
      this.editoras = response;
    })
  }

  initializeForm() {
    this.editoraForm = this.formBuild.group({
      nome: ['', [Validators.required, Validators.minLength(2)]],
      pais: ['', Validators.required],
      cidade: ['', Validators.required],
      descricao: ['', Validators.maxLength(1000)],
    });
  }

  limparCampos() {
    this.editoraForm.reset();
  }

  preecherCampos(editora: Editora) {
    this.editoraForm.controls['nome'].setValue(editora.nome);
    this.editoraForm.controls['pais'].setValue(editora.pais);
    this.editoraForm.controls['cidade'].setValue(editora.cidade);
    this.editoraForm.controls['descricao'].setValue(editora.descricao);
  }

  get f() {return this.editoraForm.controls}

  fecharModal(referencia:string) {
    let btn_fechar: HTMLElement = document .getElementById(referencia) as HTMLElement;
    btn_fechar.click();
  }

  clicarAdd() {
    this.limparCampos();
    this.mostrarBtnAdd = true;
    this.mostrarBtnAtualizar = false;
  }

  clicarRemover(editora: Editora) {
    this.editora = editora;
  }

  clicarDetalhes(editora: Editora) {
    this.editora = editora;
  }

  clicarAtualizar(editora: Editora) {
    this.preecherCampos(editora);
    this.editora = editora;
    this.mostrarBtnAtualizar = true;
    this.mostrarBtnAdd = false;
  }

  adicionar() {
    this.editoraService.adicionar(this.editoraForm.value).subscribe(response => {
      this.fecharModal('modal_fechar');
      this.limparCampos();
      this.listaEditoras();
      this.toastr.success("Editora adicionada com sucesso!");
    }, error => {
      this.modelStateErrors = error;
    })
  }

  atualizar() {
    this.editoraService.atualizar(this.editora.id,this.editoraForm.value).subscribe(response => {
      this.fecharModal('modal_fechar');
      this.limparCampos();
      this.listaEditoras();
      this.toastr.success("Editora atualizada com sucesso!");
    }, error => {
      this.modelStateErrors = error;
    })
  }

  remover() {
    this.editoraService.remover(this.editora.id).subscribe(response => {
      this.fecharModal('eliminar_fechar');
      this.listaEditoras();
      this.toastr.success("Editora removida com sucesso!");
    })
  }

}
