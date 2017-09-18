import {Component} from "@angular/core";
import {TagsService} from "./tags.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./tag-edit-page.component.html",
    styleUrls: ["./tag-edit-page.component.css"],
    selector: "ce-tag-edit-page"
})
export class TagEditPageComponent {
    constructor(private _tagsService: TagsService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.tag = (await this._tagsService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).tag;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._tagsService.addOrUpdate({ tag: $event.detail.tag, correlationId }).subscribe();
        this._router.navigateByUrl("/tags");
    }

    public tag = {};
}
