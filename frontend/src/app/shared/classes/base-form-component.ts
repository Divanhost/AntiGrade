import { BaseComponent } from './base-component.component';
import { FormGroup } from '@angular/forms';

export class BaseFormComponent extends BaseComponent {

    public isSubmiting = false;
    public form: FormGroup;
    public get controls() { return this.form.controls; }

    constructor() {
        super();
    }

    isInputHasErrors(input) {
        return this.isSubmiting && input.errors;
    }
}
