//
//  ViewController.swift
//  Calculator
//
//  Created by student on 04.03.16.
//  Copyright © 2016 student. All rights reserved.
//

import UIKit

class ViewController: UIViewController {
    
    
    // Инициализируем переменные
    var in_fst = 0.0;
    var in_snd = 0.0;
    var input: String = "";
    var oper: String = "";
    var isEnd = false;
    var canInDot = true;
    
    
    // Обработка полей вывода
    @IBOutlet weak var result: UILabel!
    
    // Обработка клавиш цифреблата
    @IBAction func add1(sender: UIButton) {
        if (isEnd)
        {
            result.text = ""
            isEnd = false
        }
        input = input + String(sender.currentTitle!)
        in_snd = Double(input)!
        result.text = result.text! + String(sender.currentTitle!)
    }
    
    
    // Обработка функциональных нажатий
    @IBAction func count(sender: UIButton) {
        if (sender.titleLabel?.text! == "del")
        {
            input = ""
            result.text = ""
            in_fst = 0.0;
            in_snd = 0.0;
            oper = ""
            canInDot = true;
        }
        else if (sender.titleLabel?.text! == "<--")
        {
            if (input.characters.count > 0) {
            input = input.substringToIndex(input.endIndex.predecessor())
            result.text = result.text!.substringToIndex(result.text!.endIndex.predecessor())
            }
        }
        else if (sender.titleLabel?.text! == ",")
        {
            input = input + "."
            result.text = result.text! + "."
            canInDot = false;
        }
        else if (sender.titleLabel?.text! != "=")
        {
            in_fst = in_snd
            oper = (sender.titleLabel?.text!)!;
            result.text = result.text! + " " + oper + " "
            input = ""
            canInDot = true;
        }
        else
        {
            input = ""
            canInDot = true;
            switch oper {
            case "+":
                result.text = String(in_fst) + " + " + String(in_snd) + " = " + String(in_fst + in_snd)
            case "-":
                result.text = String(in_fst) + " - " + String(in_snd) + " = " + String(in_fst - in_snd)
            case "*":
                result.text = String(in_fst) + " * " + String(in_snd) + " = " + String(in_fst * in_snd)
            case "/":
                result.text = String(in_fst) + " / " + String(in_snd) + " = " + String(in_fst / in_snd)
            default:
                result.text = "What?"
            }
            isEnd = true;
        }
    }
    
    
    // Встроенные функции
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }


}

