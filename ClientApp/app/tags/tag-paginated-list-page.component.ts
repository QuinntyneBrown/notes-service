import {Component, ChangeDetectorRef, NgZone} from "@angular/core";
import {TagsService} from "./tags.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./tag-paginated-list-page.component.html",
    styleUrls: ["./tag-paginated-list-page.component.css"],
    selector: "ce-tag-paginated-list-page"   
})
export class TagPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _tagsService: TagsService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router,
        private _ngZone: NgZone
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {                  
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Tags] TagAddedOrUpdated") {
                this._ngZone.run(() => {
                    this._tagsService.get().toPromise().then(x => {
                        this.unfilteredTags = x.tags;
                        this.tags = this.filterTerm != null ? this.filteredTags : this.unfilteredTags;
                    });
                });
            } else if (x.type == "[Tags] TagAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredTags = (await this._tagsService.get().toPromise()).tags;   
        this.tags = this.filterTerm != null ? this.filteredTags : this.unfilteredTags;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredTags = pluckOut({
            items: this.unfilteredTags,
            value: $event.detail.tag.id
        });

        this.tags = this.filterTerm != null ? this.filteredTags : this.unfilteredTags;
        
        this._tagsService.remove({ tag: $event.detail.tag, correlationId }).subscribe();
    }

    public tryToEdit($event) {
        this._router.navigate(["tags", $event.detail.tag.id]);
    }

    public handleTagsFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.tags = this.filterTerm != null ? this.filteredTags : this.unfilteredTags;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _tags: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public tags: Array<any> = [];
    public unfilteredTags: Array<any> = [];
    public get filteredTags() {
        return this.unfilteredTags.filter((x) => x.name.toLowerCase().indexOf(this.filterTerm.toLowerCase()) > -1);
    }
}
