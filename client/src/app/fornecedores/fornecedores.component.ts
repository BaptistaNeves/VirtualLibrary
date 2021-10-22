import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Fornecedor } from '../_models/fornecedor';
import { FornecedorService } from '../_services/fornecedor.service';

@Component({
  selector: 'app-fornecedores',
  templateUrl: './fornecedores.component.html',
  styleUrls: ['./fornecedores.component.css']
})
export class FornecedoresComponent implements OnInit {
  fornecedores: any;
  fornecedor: Fornecedor;
  modelStateErrors: any;
  fornecedorForm: FormGroup;
  btnAdicionar: boolean;
  btnAtualizar: boolean;

  constructor(private fornecedorService: FornecedorService, 
              private formBuilder: FormBuilder, 
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.listarFornecedores();
    this.initializeForm();
  }

  listarFornecedores() {
    this.fornecedorService.obterTodos().subscribe(response => {
      this.fornecedores = response;
    })
  }

  initializeForm() {
    this.fornecedorForm = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required, Validators.minLength(9), Validators.maxLength(9)]],
      endereco: ['', Validators.required],
      tipoDocumento: ['', Validators.required],
      numeroDocumento: ['', [Validators.required, Validators.minLength(13), Validators.maxLength(13)]],
      dataNascimento: ['', Validators.required]
    });
  }

  limparCampos() {
    this.fornecedorForm.reset();
  }

  preencherCampos(fornecedor: Fornecedor) {
    this.fornecedorForm.controls['nome'].setValue(fornecedor.nome);
    this.fornecedorForm.controls['email'].setValue(fornecedor.email);
    this.fornecedorForm.controls['telefone'].setValue(fornecedor.telefone);
    this.fornecedorForm.controls['endereco'].setValue(fornecedor.endereco);
    this.fornecedorForm.controls['tipoDocumento'].setValue(fornecedor.tipoDocumento);
    this.fornecedorForm.controls['numeroDocumento'].setValue(fornecedor.numeroDocumento);
    this.fornecedorForm.controls['dataNascimento'].setValue(fornecedor.dataNascimento);
  }

  fecharModal(referencia:string) {
    let btn_fechar: HTMLElement = document.getElementById(referencia) as HTMLElement;
    btn_fechar.click();
  }

  clickAdicionar() {
    this.modelStateErrors = null;
    this.btnAdicionar = true;
    this.btnAtualizar = false;
    this.limparCampos();
  }

  clickRemover(fornecedor: Fornecedor) {
    this.modelStateErrors = null;
    this.fornecedor = fornecedor;
  }

  clickDetalhes(fornecedor: Fornecedor) {
    this.fornecedor = fornecedor;
  }

  clickAtualizar(fornecedor: Fornecedor) {
    this.modelStateErrors = null;
    this.btnAtualizar = true;
    this.btnAdicionar = false;
    this.fornecedor = fornecedor;
    this.preencherCampos(fornecedor);
  }

  get f(){return this.fornecedorForm.controls;}

  adicionar() {
    this.fornecedorService.adicionar(this.fornecedorForm.value).subscribe(() => {
      this.fecharModal('modal_fechar');
      this.limparCampos();
      this.toastr.success("Fornecedor Adicionado com Sucesso!");
      this.listarFornecedores();
    }, error => {
      this.modelStateErrors = error;
    })
  }

  atualizar() {
    this.fornecedorService.atualizar(this.fornecedor.id, this.fornecedorForm.value).subscribe(() => {
      this.fecharModal('modal_fechar');
      this.limparCampos();
      this.toastr.success("Fornecedor Atualizado com Sucesso!");
      this.listarFornecedores();
    }, error => {
      this.modelStateErrors = error;
    })
  }

  remover() {
    this.fornecedorService.remover(this.fornecedor.id).subscribe(() => {
      this.fecharModal('eliminar_fechar');
      this.toastr.success("Fornecedor Removido com Sucesso!");
      this.listarFornecedores();
    }, error => {
      this.modelStateErrors = error;
    })
  }
}
