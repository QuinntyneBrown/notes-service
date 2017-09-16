import {Component, ChangeDetectorRef, NgZone} from "@angular/core";
import {NotesService} from "./notes.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./note-paginated-list-page.component.html",
    styleUrls: ["./note-paginated-list-page.component.css"],
    selector: "ce-note-paginated-list-page"   
})
export class NotePaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _notesService: NotesService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router,
        private _ngZone: NgZone
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {                  
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Notes] NoteAddedOrUpdated") {                
                this._notesService.get().toPromise().then(x => {
                    this.unfilteredNotes = x.notes;
                    this.notes = this.filterTerm != null ? this.filteredNotes : this.unfilteredNotes;                        
                });
            } else if (x.type == "[Notes] NoteAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredNotes = (await this._notesService.get().toPromise()).notes;   
        this.notes = this.filterTerm != null ? this.filteredNotes : this.unfilteredNotes;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredNotes = pluckOut({
            items: this.unfilteredNotes,
            value: $event.detail.note.id
        });

        this.notes = this.filterTerm != null ? this.filteredNotes : this.unfilteredNotes;
        
        this._notesService.remove({ note: $event.detail.note, correlationId }).subscribe();
    }

    public tryToEdit($event) {
        this._router.navigate(["notes", $event.detail.note.id]);
    }

    public handleNotesFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.notes = this.filterTerm != null ? this.filteredNotes : this.unfilteredNotes;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _notes: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public notes: Array<any> = [];
    public unfilteredNotes: Array<any> = [];
    public get filteredNotes() {
        return this.unfilteredNotes.filter((x) => x.body.toLowerCase().indexOf(this.filterTerm.toLowerCase()) > -1);
    }
}
