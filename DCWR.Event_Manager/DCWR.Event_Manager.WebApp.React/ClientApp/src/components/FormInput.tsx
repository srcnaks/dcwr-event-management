import * as React from 'react';

const formInput = (props: any) => {
    return (
        <div className='form-group'>
            <label htmlFor={props.name}>{props.label}</label>
            <input type="text"
                className="form-control"
                name={props.name}
                value={props.value}
                onChange={props.handleChange} />
        </div>
        );
}

export default formInput;