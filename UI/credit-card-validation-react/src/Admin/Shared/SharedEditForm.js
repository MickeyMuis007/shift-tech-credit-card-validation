import React from "react";
import Form from "@rjsf/core";



export class SharedEditForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
    }
  }

  componentDidMount() {
  }

  render() {
    return (
    <React.Fragment>
      <Form schema={this.props.schema} formData={this.props.formData} noHtml5Validate onSubmit={this.props.onSubmit} showErrorList={false} />
    </React.Fragment>
    )
  }
}