//
import Entity from '../Entity';

// Utils
import { cdnPath } from '../../utils/html';

/**
 * @class AccountEntity
 */
export default class AccountEntity extends Entity
{
  /** @var {String} */
  static defaultImgBase64 = 'data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%2264%22%20height%3D%2264%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%2064%2064%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_16caa47cdd6%20text%20%7B%20fill%3A%23AAAAAA%3Bfont-weight%3Abold%3Bfont-family%3AArial%2C%20Helvetica%2C%20Open%20Sans%2C%20sans-serif%2C%20monospace%3Bfont-size%3A10pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_16caa47cdd6%22%3E%3Crect%20width%3D%2264%22%20height%3D%2264%22%20fill%3D%22%23EEEEEE%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%2213.171875%22%20y%3D%2236.5%22%3E64x64%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E';

  /**
   * @param {String} dpu
   * @returns {String}
   */
  static defaultPictureUrlStatic(dpu)
  {
    return cdnPath(dpu || _static.defaultImgBase64);
  }

  /**
   * @var {String} Primary Key
   */
  primaryKey = 'member_id';

  /**
   * 
   * @param {object} data 
   */
  // constructor(data) { super(data); }

  /**
   * 
   * @returns {String}
   */
  key()
  {
    let key = this.tel || Math.random().toString();
    return key;
  }

  /**
   * 
   * @returns {String}
   */
  _fullname()
  {
    return this.fullname || [
      this.first_name,
      this.last_name
    ].join(' ').trim();
  }

  /**
   * 
   * @returns {String}
   */
  defaultPictureUrl()
  {
    return _static.defaultPictureUrlStatic(this.image_avatar);
  }

  /**
   * @TODO:
   * @returns {Boolean}
   */
  _isAdministrator()
  {
    let prop = 'isAdministrator';
    if (prop in this) {
      return !!this[prop];
    }
    return ('administrator' === this.user_name);
  }

  /**
   * @TODO:
   * @returns {Array}
   */
  getFunctions()
  {
    return this._functions || [];
  }
}
// Make alias
const _static = AccountEntity;