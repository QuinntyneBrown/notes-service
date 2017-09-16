import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./note-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./note-list-item.component.css"
    ],
    selector: "ce-note-list-item"
})
export class NoteListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();		
    }
      
    @Input()
    public note: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
