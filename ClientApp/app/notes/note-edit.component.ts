import {
    Component,
    Input,
    OnInit,
    EventEmitter,
    Output,
    AfterViewInit,
    AfterContentInit,
    Renderer,
    ElementRef,
} from "@angular/core";

import {FormGroup,FormControl,Validators} from "@angular/forms";

@Component({
    templateUrl: "./note-edit.component.html",
    styleUrls: [
        "../../styles/forms.css",
        "../../styles/edit.css",
        "./note-edit.component.css"],
    selector: "ce-note-edit"
})
export class NoteEditComponent {
    constructor() {
        this.tryToSave = new EventEmitter();
    }

    @Output()
    public tryToSave: EventEmitter<any>;

    private _note: any = {};

    @Input("note")
    public set note(value) {
        this._note = value;

        this.form.patchValue({
            id: this._note.id,
            title: this._note.title,
            body: this._note.body
        });
    }
   
    public form = new FormGroup({
        id: new FormControl(0, []),
        title: new FormControl('', [Validators.required]),
        body: new FormControl('')
    });
}
