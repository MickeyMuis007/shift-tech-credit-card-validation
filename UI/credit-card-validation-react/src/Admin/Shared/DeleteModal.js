import React from 'react';
import Modal from '@material-ui/core/Modal';
import Backdrop from '@material-ui/core/Backdrop';
import Fade from '@material-ui/core/Fade';
import Button from "@material-ui/core/Button"

export default function CreditCardStatusModal(props) {
  const selectedItem = props.selected || {};
  return (
    <div>
      <Modal
        className="container-center"
        aria-labelledby="transition-modal-title"
        aria-describedby="transition-modal-description"
        open={props.open}
        onClose={props.onClose}
        closeAfterTransition
        BackdropComponent={Backdrop}
        BackdropProps={{
          timeout: 500,
        }}>
        <Fade in={props.open}>
          <div className="p-4 bg-light text-center">
            <h3>Are you sure want to delete</h3>
            <h4 className="font-weight-bold">{selectedItem.name}?</h4>
            <Button onClick={props.onClose} style={{outline:"none"}}>No</Button>
            <Button color="secondary" style={{outline:"none"}} onClick={() => props.onDelete(selectedItem.id)}>Yes</Button>
          </div>
        </Fade>
      </Modal>
    </div>
  );
}
