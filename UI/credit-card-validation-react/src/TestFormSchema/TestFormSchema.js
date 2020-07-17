import React from "react";
import Form from "@rjsf/core";

const schema = {
  "title": "Identity",
  "type": "object",
  "required": [
    "firstName",
    "lastName"
  ],
  "properties": {
    "firstName": {
      "type": "string",
      "title": "First name",
      "minLength": 1,
      "maxLength": 6
    },
    "lastName": {
      "type": "string",
      "title": "Last name"
    },
    "age": {
      "type": "number",
      "title": "Age"
    }
  }
};

export class TestFormSchema extends React.Component
{
  constructor() {
    super();
    this.state = {

    }
  }

  render() {
    return <Form schema={schema} noHtml5Validate onSubmit={console.log} showErrorList={false} />
  }
}