import {Component} from "@angular/core";
import {NotesService} from "./notes.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./note-edit-page.component.html",
    styleUrls: ["./note-edit-page.component.css"],
    selector: "ce-note-edit-page"
})
export class NoteEditPageComponent {
    constructor(private _notesService: NotesService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.note = (await this._notesService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).note;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._notesService.addOrUpdate({ note: $event.detail.note, correlationId }).subscribe();
        this._router.navigateByUrl("/notes");
    }

    public note = {};
}
