import React from "react";
import { MDBPagination, MDBPageItem, MDBPageNav, MDBCol, MDBRow, MDBBadge, MDBBtn } from "mdbreact";

export const PageConstants = {
  FIRST: "FIRST",
  PREVIOUS: "PREVIOUS",
  CURRENT: "CURRENT",
  NEXT: "NEXT",
  LAST: "LAST"
}

export const Pagination = ({ onChange, metaData }) => {
  console.log("Pagination data:", metaData);
  return (
    <MDBRow>
      <MDBCol>
        <MDBPagination circle>
          <MDBPageItem disabled={true}>
            <MDBPageNav className="page-link" onClick={() => onChange(PageConstants.FIRST)}>
              <span>First</span>
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem disabled={true}>
            <MDBPageNav className="page-link" aria-label="Previous">
              <span aria-hidden="true">&laquo;</span>
              <span className="sr-only">Previous</span>
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem active>
            <MDBPageNav className="page-link">
              1 <span className="sr-only">(current)</span>
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem>
            <MDBPageNav className="page-link">
              2
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem>
            <MDBPageNav className="page-link">
              3
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem>
            <MDBPageNav className="page-link">
              4
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem>
            <MDBPageNav className="page-link">
              5
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem>
            <MDBPageNav className="page-link">
              &raquo;
            </MDBPageNav>
          </MDBPageItem>
          <MDBPageItem>
            <MDBPageNav className="page-link">
              Last
            </MDBPageNav>
          </MDBPageItem>
        </MDBPagination>
      </MDBCol>
      <MDBBtn color="info" disabled><span>Total: { metaData.totalCount}</span></MDBBtn>
    </MDBRow>
    )
}