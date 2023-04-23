import React from 'react';
import ko from 'knockout';
import 'devexpress-reporting/dx-webdocumentviewer';

export default class PostsReportViewer extends React.Component {
    constructor(props) {
        super(props);
        this.reportUrl = ko.observable("PostReport");
        this.requestOptions = {
            host: "https://localhost:7248/",
            invokeAction: "DXXRDV"
        };
    }
    render() {
        return (<div style={{ width: "100%", height: "1000px" }} ref="viewer" data-bind="dxReportViewer: $data"></div>);
    }
    componentDidMount() {
        ko.applyBindings({
            reportUrl: this.reportUrl,
            requestOptions: this.requestOptions
        }, this.refs.viewer);
    }
    componentWillUnmount() {
        ko.cleanNode(this.refs.viewer);
    }
};