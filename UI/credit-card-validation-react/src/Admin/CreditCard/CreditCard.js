import React from "react";
import { Container, Table, Card } from "react-bootstrap";

export class CreditCard extends React.Component {
  constructor() {
    super();
    this.state = {

    }
  }

  render() {
    return (
      <Container>
        <Card className="main-card">
          <Card.Header as="h5">Credit Card</Card.Header>
          <Card.Body>
            <Table striped bordered hover variant="dark">
              <thead>
                <tr>
                  <th>#</th>
                  <th>First Name</th>
                  <th>Last Name</th>
                  <th>Username</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>1</td>
                  <td>Mark</td>
                  <td>Otto</td>
                  <td>@mdo</td>
                </tr>
                <tr>
                  <td>2</td>
                  <td>Jacob</td>
                  <td>Thornton</td>
                  <td>@fat</td>
                </tr>
                <tr>
                  <td>3</td>
                  <td colSpan="2">Larry the Bird</td>
                  <td>@twitter</td>
                </tr>
              </tbody>
            </Table>

          </Card.Body>
        </Card>
      </Container>
    )
  }
}